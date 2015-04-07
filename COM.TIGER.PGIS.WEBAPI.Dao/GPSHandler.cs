using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COM.TIGER.PGIS.WEBAPI.Model;
using COM.TIGER.PGIS.WEBAPI.IDao;
using System.Transactions;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// GPS 单兵作战模块处理程序
    /// </summary>
    public class GPSHandler:DBase
    {
        private static GPSHandler _instance;
        public static GPSHandler Handler
        {
            get { return _instance = _instance ?? new GPSHandler(); }
        }
        GPSHandler() { }

        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0)
                return 0;

            int result = 0;
            TransactionScope tran = null;
            try
            {
                tran = new TransactionScope();

                IDao.IDbase query = DeleteHandler.From<GpsDevice>()
                    .Where<GpsDevice>(t => t.DeviceID.In(ids))
                    .Execute();
                result += query.ExecuteNonQuery();

                query = DeleteHandler.From<GpsDeviceTrack>()
                    .Where<GpsDeviceTrack>(t => t.DeviceID.In(ids))
                    .Execute();
                result += query.ExecuteNonQuery();

                tran.Complete();
            }
            catch (Exception) { result = 0; }
            finally
            {
                if (tran != null)
                    tran.Dispose();
            }

            return result;
        }

        public override int InsertEntity(object obj)
        {
            GpsDevice t = obj as GpsDevice;
            if (t == null)
                return 0;

            GpsDevice d = GetEntity<GpsDevice>(x => x.DeviceID == t.DeviceID || x.OfficerID == t.OfficerID || x.CarNum == t.CarNum);
            if (d != null)
                return -2;

            t.BindTime = DateTime.Now;
            return InsertHandler.Into<GpsDevice>()
                .Table("BindTime", "CarID", "CarNum", "DeviceID", "OfficerID")
                .Values(t.BdTime, t.CarID, t.CarNum, t.DeviceID, t.OfficerID)
                .Execute()
                .ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            GpsDevice t = obj as GpsDevice;
            if(t == null)
                return 0;

            GpsDevice d = GetEntity<GpsDevice>(x => (x.DeviceID == t.DeviceID || x.OfficerID == t.OfficerID || x.CarNum == t.CarNum) && x.ID != t.ID);
            if (d != null)
                return -2;

            return UpdateHandler.Table<GpsDevice>()
                .Set("BindTime").EqualTo(t.BdTime)
                .Set("CarID").EqualTo(t.CarID)
                .Set("CarNum").EqualTo(t.CarNum)
                .Set("DeviceID").EqualTo(t.DeviceID)
                .Set("OfficerID").EqualTo(t.OfficerID)
                .Where<GpsDevice>(x => x.ID == t.ID)
                .Execute()
                .ExecuteNonQuery();
        }

        public int InsertDeviceTrack(GpsDeviceTrack t)
        {
            if (t == null)
                return 0;

            return InsertHandler.Into<GpsDeviceTrack>()
                .Table("CurrentTime", "DeviceID", "OfficerNum", "X", "Y")
                .Values(t.CurrTime, t.DeviceID, t.OfficerNum, t.X, t.Y)
                .Execute()
                .ExecuteNonQuery();
        }

        public List<GpsDevice> PageDevices(int index, int size, out int records)
        {
            return Paging<GpsDevice>(
                index,
                size,
                null,
                OrderType.Desc,
                out records,
                string.Format("{0}.ID", GetTableName<GpsDevice>()));
        }

        public List<GpsDevice> PageDevicesAtNumber(int index, int size, string number, out int records)
        {
            return Paging<GpsDevice>(
                index,
                size,
                t => t.DeviceID.Like(number),
                OrderType.Desc,
                out records,
                string.Format("{0}.ID", GetTableName<GpsDevice>()));
        }

        public List<GpsDevice> PageDevicesAtOfficer(int index, int size, string officerid, out int records)
        {
            return Paging<GpsDevice>(
                index,
                size,
                t => t.OfficerID.Like(officerid),
                OrderType.Desc,
                out records,
                string.Format("{0}.ID", GetTableName<GpsDevice>()));
        }

        public List<GpsDevice> PageDevicesAtCar(int index, int size, string carid, out int records)
        {
            return Paging<GpsDevice>(
                index,
                size,
                t => t.CarNum.Like(carid),
                OrderType.Desc,
                out records,
                string.Format("{0}.ID", GetTableName<GpsDevice>()));
        }

        /// <summary>
        /// 获取指定设备号码的设备绑定信息
        /// </summary>
        /// <param name="deviceIds"></param>
        /// <returns></returns>
        protected List<GpsDevice> GetDevices(params string[] deviceIds)
        {
            if (deviceIds.Length == 0)
                return new List<GpsDevice>();

            return GetEntities<GpsDevice>(t => t.DeviceID.In(deviceIds));
        }

        public List<GpsDeviceTrack> GetDevicesCurrentPosition()
        {
            string tablename = GetTableName<GpsDeviceTrack>();
            string sqlStr = string.Format("select distinct deviceid, max(id) as id from {0} group by deviceid", tablename);
            sqlStr = string.Format("select {0}.* from {0} inner join ({1}) as t on {0}.id = t.id", tablename, sqlStr);
            IDbase db = new Dbase(sqlStr);

            List<GpsDeviceTrack> list = ExecuteList<GpsDeviceTrack>(db.ExecuteDataReader());
            SetDevice(ref list);
            return list;
        }

        public List<GpsDeviceTrack> GetDeviceHistoryPoints(string deviceId, DateTime start, DateTime end)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return new List<GpsDeviceTrack>();

            List<GpsDeviceTrack> list = GetEntities<GpsDeviceTrack>(t =>
                t.DeviceID.Like(deviceId)
                && t.CurrentTime >= start
                && t.CurrentTime <= end);
            SetDevice(ref list);

            return list;
        }

        public List<GpsDeviceTrack> GetDevicesCurrentPostionAtPanel(double x1, double y1, double x2, double y2)
        {
            List<GpsDeviceTrack> list = GetDevicesCurrentPosition();
            list = list.Where(t => t.X >= x1 && t.Y >= y1 && t.X <= x2 && t.Y <= y2).ToList();
            return list;
        }

        public List<GpsDeviceTrack> GetDevicesCurrentPostionAtPanel(double[] coords, double x1, double y1, double x2, double y2)
        {
            List<GpsDeviceTrack> list = GetDevicesCurrentPosition();
            var poly = GetPoints(coords);
            list = list.Where(t => WEBAPI.Common.GDI.GDIHelper.PointInPoly(poly, new Common.GDI.Point() { X = t.X, Y = t.Y })).ToList();
            return list;
        }

        private void SetDevice(ref List<GpsDeviceTrack> list)
        {
            IEnumerable<string> ids = from t in list select t.DeviceID;
            List<GpsDevice> devices = GetDevices(ids.ToArray());
            list.ForEach(t => 
            {
                t.Device = devices.FirstOrDefault(x => x.DeviceID == t.DeviceID);
            });
        }
    }
}

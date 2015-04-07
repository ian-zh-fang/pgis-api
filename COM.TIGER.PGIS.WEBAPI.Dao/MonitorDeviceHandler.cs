using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class MonitorDeviceHandler:DBase
    {
        private MonitorDeviceHandler() { }
        private static MonitorDeviceHandler _instance;
        public static MonitorDeviceHandler Handler
        {
            get { return _instance = _instance ?? new MonitorDeviceHandler(); }
        }

        public override int InsertEntity(object obj)
        {
            var e = obj as Model.MonitorDevice;
            if (e == null) 
                return 0;

            if (GetEntity<Model.MonitorDevice>(t => t.Num == e.Num) != null)
                return -2;

            // 匹配地址
            var address = CheckAddress(e.AdministrativeID, e.AdministrativeName, e.AddressContext);
            //地址添加失败
            if (address == null)
                return -1;

            e.AddressID = address.ID;
            return InsertHandler.Into<Model.MonitorDevice>()
                .Table("Y", "X", "Pwd", "Port", "Num", "Name", "IP", "DoTypeName", "DoTypeID", "DeviceTypeName", "DeviceTypeID", "DeviceID", "AddressID", "Acct")
                .Values(e.Y, e.X, e.Pwd, e.Port, e.Num, e.Name, e.IP, e.DoTypeName, e.DoTypeID, e.DeviceTypeName, e.DeviceTypeID, e.DeviceID, e.AddressID, e.Acct)
                .Execute()
                .ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.MonitorDevice;
            if (e == null) 
                return 0;

            if (null != GetEntity<Model.MonitorDevice>(t => t.Num == e.Num && t.ID != e.ID))
                return -2;

            // 匹配地址
            var address = CheckAddress(e.AdministrativeID, e.AdministrativeName, e.AddressContext);
            //地址添加失败
            if (address == null)
                return -1;

            e.AddressID = address.ID;
            var query = UpdateHandler.Table<Model.MonitorDevice>()
                        .Set("Acct").EqualTo(e.Acct)
                        .Set("AddressID").EqualTo(e.AddressID)
                        .Set("DeviceID").EqualTo(e.DeviceID)
                        .Set("DeviceTypeID").EqualTo(e.DeviceTypeID)
                        .Set("DeviceTypeName").EqualTo(e.DeviceTypeName)
                        .Set("DoTypeID").EqualTo(e.DoTypeID)
                        .Set("DoTypeName").EqualTo(e.DoTypeName)
                        .Set("IP").EqualTo(e.IP)
                        .Set("Name").EqualTo(e.Name)
                        .Set("Num").EqualTo(e.Num)
                        .Set("Port").EqualTo(e.Port)
                        .Set("Pwd").EqualTo(e.Pwd)
                        .Set("X").EqualTo(e.X)
                        .Set("Y").EqualTo(e.Y)
                        .Where<Model.MonitorDevice>(t => t.ID == e.ID);
            return query.Execute().ExecuteNonQuery();
        }

        public override int DeleteEntities(params string[] ids)
        {
            return DeleteHandler.From<Model.MonitorDevice>().Where<Model.MonitorDevice>(t => t.ID.In(ids)).Execute().ExecuteNonQuery();
        }

        public List<Model.MonitorDevice> Page(int index, int size, out int records)
        {
            var list = Paging<Model.MonitorDevice>(index, size, null, OrderType.Desc, out records, "Pgis_MonitorDevice.ID");
            GetDeviceAddress(ref list);
            return list;
        }

        public List<Model.MonitorDevice> PageQuery(string name, string num, int dotypeid, string address, int index, int size, out int records)
        {
            var query = GetPageQuery(GetTableName<Model.MonitorDevice>(), "ID");
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where<Model.MonitorDevice>(t => t.Name.Like(name));
            }
            if (!string.IsNullOrWhiteSpace(num))
            {
                query = query.Where<Model.MonitorDevice>(t => t.Num == num);
            }
            if (dotypeid > 0)
            {
                query = query.Where<Model.MonitorDevice>(t => t.DoTypeID == dotypeid);
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Join(JoinType.Inner, GetTableName<Model.Address>())
                    .On(string.Format("{0}.ID = {1}.AddressID", GetTableName<Model.Address>(), GetTableName<Model.MonitorDevice>()));
                MatchAddress(address, ref query);
            }
            var list = Paging<Model.MonitorDevice>(query, index, size, out records);
            GetDeviceAddress(ref list);
            return list;
        }

        public List<Model.Address> Match(string pattern)
        {
            if(string.IsNullOrWhiteSpace(pattern))
                return new List<Model.Address>();

            var patterns = pattern.ToCharArray();
            var query = SelectHandler.Columns("top 10 Pgis_Address.*").From<Model.Address>()
                .Join(JoinType.Inner, "Pgis_MonitorDevice").On("Pgis_Address.ID = Pgis_MonitorDevice.AddressID");
            for (var i = 0; i < patterns.Length; i++)
            {
                var str = string.Format("{0}", patterns[i]);
                if (string.IsNullOrWhiteSpace(str))
                    continue;

                query = query.Where<Model.Address>(t => t.Content.Like(str));
            }
            return ExecuteList<Model.Address>(query.Execute().ExecuteDataReader());
        }

        public List<Model.MonitorDevice> GetDevices()
        {
            var query = SelectHandler.From<Model.MonitorDevice>();
            return ExecuteList<Model.MonitorDevice>(query.Execute().ExecuteDataReader());
        }

        public List<Model.MonitorDevice> GetDevices(params string[] ids)
        {
            if (ids.Length == 0)
                return new List<Model.MonitorDevice>();

            var list = GetEntities<Model.MonitorDevice>(t => t.ID.In(ids));
            
            GetDeviceAddress(ref list);

            return list;
        }

        /// <summary>
        /// 获取指定座标内的所有监控信息
        /// </summary>
        /// <param name="x1">左上角座标横坐标</param>
        /// <param name="y1">左上角座标纵坐标</param>
        /// <param name="x2">右下角座标横坐标</param>
        /// <param name="y2">右下角座标纵坐标</param>
        /// <returns></returns>
        public List<Model.MonitorDevice> Query(double x1, double y1, double x2, double y2)
        {
            var list = GetEntities<Model.MonitorDevice>(t => t.X >= x1 && t.X <= x2 && t.Y >= y1 && t.Y <= y2);
            GetDeviceAddress(ref list);
            return list;
        }

        /// <summary>
        /// 获取指定座标内的所有监控信息
        /// </summary>
        /// <param name="coords">不规则多边形的各端点座标</param>
        /// <param name="x1">左上角座标横坐标</param>
        /// <param name="y1">左上角座标纵坐标</param>
        /// <param name="x2">右下角座标横坐标</param>
        /// <param name="y2">右下角座标纵坐标</param>
        /// <returns></returns>
        public List<Model.MonitorDevice> Query(double[] coords, double x1, double y1, double x2, double y2)
        {
            var list =  GetEntities<Model.MonitorDevice>(t => t.X >= x1 && t.X <= x2 && t.Y >= y1 && t.Y <= y2);
            var poly = GetPoints(coords);
            list = list.Where(t => WEBAPI.Common.GDI.GDIHelper.PointInPoly(poly, new Common.GDI.Point() { X = t.X, Y = t.Y })).ToList();
            GetDeviceAddress(ref list);
            return list;
        }

        /// <summary>
        /// 校验详细地址
        /// </summary>
        /// <param name="adminID">指定的行政区划标识符</param>
        /// <param name="adminID">指定的行政区划名称</param>
        /// <param name="addressContext">详细地址</param>
        /// <returns></returns>
        private Model.Address CheckAddress(int adminID, string adminName, string addressContext)
        {
            if (string.IsNullOrWhiteSpace(addressContext))
                addressContext = adminName;

            var address = GetEntity<Model.Address>(t => t.AdminID == adminID && t.Content == addressContext);
            if (address == null)
            {
                address = new Model.Address() { AdminID = adminID, Content = addressContext };
                Dao.AddressHandler.Handler.Add(address);
                address = GetEntity<Model.Address>(t => t.AdminID == adminID && t.Content == addressContext);
            }
            return address;
        }

        /// <summary>
        /// 获取设备地址信息
        /// </summary>
        /// <param name="devices"></param>
        private void GetDeviceAddress(ref List<Model.MonitorDevice> devices)
        {
            var ids = (from t in devices select t.AddressID.ToString()).ToArray();
            var addresses = Dao.AddressHandler.Handler.GetEntities(ids);
            devices.ForEach(t =>
            {
                t.Address = addresses.FirstOrDefault(x => x.ID == t.AddressID);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 酒店，宾馆，旅店信息处理模块
    /// </summary>
    public class HotelHandler:DBase
    {
        private static HotelHandler _instance = null;
        public static HotelHandler Handler { get { return _instance = _instance ?? new HotelHandler(); } }
        private HotelHandler() { }

        public List<Model.Hotel> PageHotels(int index, int size, out int records)
        {
            var list = Paging<Model.Hotel>(index, size, null, OrderType.Desc, out records, string.Format("{0}.ID", GetTableName<Model.Hotel>()));
            GetHotelAddress(ref list);
            return list;
        }

        public List<Model.HotelStay> GetOnHotel(int id, int index, int size, out int records)
        {
            var list = Paging<Model.HotelStay>(index, size, t => t.HotelID == id, OrderType.Desc, out records, string.Format("{0}.ID", GetTableName<Model.HotelStay>()));
            GetHotelStayHotel(ref list);
            return list;
        }

        public List<Model.HotelStay> GetOnRoom(int id, string roomnum, string ptime)
        {
            var query = SelectHandler.From<Model.HotelStay>()
                .Where<Model.HotelStay>(t => t.HotelID == id);

            if (!string.IsNullOrWhiteSpace(roomnum))
                query = query.Where<Model.HotelStay>(t => t.PutinRoomNum == roomnum);

            if (!string.IsNullOrWhiteSpace(ptime))
            {
                var time = DateTime.Parse(ptime);
                var tfrom = time.AddMinutes(-TimeRange).ToString("yyyy-MM-dd HH:mm:ss.fff");
                var tend = time.AddMinutes(TimeRange).ToString("yyyy-MM-dd HH:mm:ss.fff");
                var tname = GetTableName<Model.HotelStay>();
                query = query.Where(string.Format("{0}.PutinTime >= '{1}' and {0}.PutinTime <= '{2}'", tname, tfrom, tend));
            }

            var list = ExecuteList<Model.HotelStay>(query.Execute().ExecuteDataReader());
            GetHotelStayHotel(ref list);
            return list;
        }

        public List<Model.HotelStay> GetOnTogether(int id, string ptime)
        {
            var query = SelectHandler.From<Model.HotelStay>()
                .Where<Model.HotelStay>(t => t.HotelID == id);

            if (!string.IsNullOrWhiteSpace(ptime))
            {
                var time = DateTime.Parse(ptime);
                var tfrom = time.AddMinutes(-TimeRange).ToString("yyyy-MM-dd HH:mm:ss.fff");
                var tend = time.AddMinutes(TimeRange).ToString("yyyy-MM-dd HH:mm:ss.fff");
                var tname = GetTableName<Model.HotelStay>();
                query = query.Where(string.Format("{0}.PutinTime >= '{1}' and {0}.PutinTime <= '{2}'", tname, tfrom, tend));
            }

            var list = ExecuteList<Model.HotelStay>(query.Execute().ExecuteDataReader());
            GetHotelStayHotel(ref list);
            return list;
        }

        public List<Model.HotelStay> GetOnMove(string code, int index, int size, out int records)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                records = 0;
                return new List<Model.HotelStay>();
            }

            var list = Paging<Model.HotelStay>(index, size, t => t.CredentialsNum == code, OrderType.Desc, out records, string.Format("{0}.PutinTime", GetTableName<Model.HotelStay>()));
            GetHotelStayHotel(ref list);
            return list;
        }

        public int UpdateAddress(int id, string addr)
        {
            //@ 首先获取指定的地址，如果不存在直接返回
            var address = AddressHandler.Handler.GetEntity(addr);
            if (address == null)
                return -100;

            return UpdateHandler.Table<Model.Hotel>()
                .Set("AddressID").EqualTo(address.ID)
                .Where<Model.Hotel>(t => t.ID == id)
                .Execute()
                .ExecuteNonQuery();
        }

        public List<Model.Hotel> MatchHotel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Model.Hotel>();

            var records = 0;
            return Paging<Model.Hotel>(1, 10, t => t.Name.Like(name), OrderType.Desc, out records, string.Format("{0}.ID", GetTableName<Model.Hotel>()));
        }

        public List<Model.Hotel> QueryHotel(string name, string addr, int index, int size, out int records)
        {
            var hname = GetTableName<Model.Hotel>();
            var query = GetPageQuery(hname, "ID");

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where<Model.Hotel>(t => t.Name.Like(name));

            if (!string.IsNullOrWhiteSpace(addr))
            {
                var aname = GetTableName<Model.Address>();
                query = query.Join(JoinType.Left, aname).On(string.Format("{0}.AddressID == {1}.ID", hname, aname))
                    .Where<Model.Address>(t => t.Content.Like(addr));
            }

            var list = Paging<Model.Hotel>(query, index, size, out records);
            GetHotelAddress(ref list);
            return list;
        }

        public List<Model.HotelStay> QueryHotelStay(string name, string code, string hname, string roomnum, string ptime, string gtime, int index, int size, out int records)
        {
            var tname = GetTableName<Model.HotelStay>();
            var query = GetPageQuery(tname, "ID");

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where<Model.HotelStay>(t => t.Name.Like(name));

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where<Model.HotelStay>(t => t.CredentialsNum.Like(code));

            if (!string.IsNullOrWhiteSpace(hname))
                query = query.Where<Model.HotelStay>(t => t.HotelName.Like(hname));

            if (!string.IsNullOrWhiteSpace(roomnum))
                query = query.Where<Model.HotelStay>(t => t.PutinRoomNum == roomnum);

            if (!string.IsNullOrWhiteSpace(ptime))
                query = query.Where(string.Format("{0}.PutinTime >= '{1}'", tname, ptime));

            if (!string.IsNullOrWhiteSpace(gtime))
                query = query.Where(string.Format("{0}.PutinTime <= '{1}'", tname, gtime));

            var list = Paging<Model.HotelStay>(query, index, size, out records);
            GetHotelStayHotel(ref list);
            return list;
        }

        private void GetHotelAddress(ref List<Model.Hotel> list)
        {
            if (list.Count == 0) 
                return;
            
            var ids = (from t in list select t.AddressID.ToString()).ToArray();
            var addresses = AddressHandler.Handler.GetEntities(ids);
            list.ForEach(t => t.Address = addresses.FirstOrDefault(x => t.AddressID == x.ID));
        }

        private void GetHotelStayHotel(ref List<Model.HotelStay> list)
        {
            if (list.Count == 0)
                return;

            var ids = (from t in list select t.HotelID.ToString()).ToArray();
            var hotels = GetEntities<Model.Hotel>(t => t.ID.In(ids));
            GetHotelAddress(ref hotels);

            list.ForEach(t => t.Hotel = hotels.FirstOrDefault(x => t.HotelID == x.ID));
        }
    }
}

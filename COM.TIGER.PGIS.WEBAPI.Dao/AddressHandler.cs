using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class AddressHandler : DBase
    {
        /// <summary>
        /// get singleton instance
        /// </summary>
        private static AddressHandler _instance = null;
        public static AddressHandler Handler { get { return _instance = _instance ?? new AddressHandler(); } }
        private AddressHandler() { }

        /// <summary>
        /// 模糊匹配地址
        /// </summary>
        /// <param name="pattern">详细地址的全部或一部</param>
        /// <returns></returns>
        public List<Model.Address> Match(string pattern)
        { 
            //拆分字符串，以“与”的关系查询所有的匹配项，并取前十
            var patterns = string.IsNullOrWhiteSpace(pattern) ? new string[0] : pattern.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            return Match(patterns);
        }

        /// <summary>
        /// 批量获取指定ID的地址信息
        /// </summary>
        /// <param name="ids">指定的ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.Address> GetEntities(params string[] ids)
        {
            if (ids.Length == 0) return new List<Model.Address>();

            var list = GetEntities<Model.Address>(t => t.ID.In(ids));

            ids = (from t in list select t.AdminID.ToString()).ToArray();
            var administratives = ids.Length == 0 ? new List<Model.Administrative>() : GetEntities<Model.Administrative>(t => t.ID.In(ids));
            
            ids = (from t in list select t.NumID.ToString()).ToArray();
            var nums = ids.Length == 0 ? new List<Model.StreetNum>() : GetEntities<Model.StreetNum>(t => t.ID.In(ids));
            
            ids = (from t in list select t.OwnerInfoID.ToString()).ToArray();
            var ownerinfos = ids.Length == 0 ? new List<Model.OwnerInfo>() : GetEntities<Model.OwnerInfo>(t => t.MOI_ID.In(ids));
            
            ids = (from t in list select t.RoomID.ToString()).ToArray();
            var rooms = ids.Length == 0 ? new List<Model.Rooms>() : GetEntities<Model.Rooms>(t => t.Room_ID.In(ids));
            
            ids = (from t in list select t.StreetID.ToString()).ToArray();
            var streets = ids.Length == 0 ? new List<Model.Street>() : GetEntities<Model.Street>(t => t.ID.In(ids));
            
            ids = (from t in list select t.UnitID.ToString()).ToArray();
            var units = ids.Length == 0 ? new List<Model.Unit>() : GetEntities<Model.Unit>(t => t.Unit_ID.In(ids));

            list.ForEach(t => {
                t.Administrative = administratives.FirstOrDefault(x => x.ID == t.AdminID);
                t.OwnerInfo = ownerinfos.FirstOrDefault(x => x.MOI_ID == t.OwnerInfoID);
                t.Room = rooms.FirstOrDefault(x => x.Room_ID == t.RoomID);
                t.Street = streets.FirstOrDefault(x => x.ID == t.StreetID);
                t.StreetNum = nums.FirstOrDefault(x => x.ID == t.NumID);
                t.Unit = units.FirstOrDefault(x => x.Unit_ID == t.UnitID);
            });

            return list;
        }

        /// <summary>
        /// 添加新的地址
        /// </summary>
        /// <param name="e">新地址</param>
        /// <returns></returns>
        public int Add(Model.Address e)
        {
            var query = InsertHandler.Into<Model.Address>()
                .Table("AdminID", "Content", "NumID", "OwnerInfoID", "RoomID", "StreetID", "UnitID")
                .Values(e.AdminID, e.Content, e.NumID, e.OwnerInfoID, e.RoomID, e.StreetID, e.UnitID);
            return query.Execute().ExecuteNonQuery();
        }

        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Address;
            if (e == null)
                return -1;

            return UpdateHandler.Table<Model.Address>()
                .Set("AdminID").EqualTo(e.AdminID)
                .Set("Content").EqualTo(e.Content)
                .Set("NumID").EqualTo(e.NumID)
                .Set("OwnerInfoID").EqualTo(e.OwnerInfoID)
                .Set("RoomID").EqualTo(e.RoomID)
                .Set("StreetID").EqualTo(e.StreetID)
                .Set("UnitID").EqualTo(e.UnitID)
                .Where<Model.Address>(t => t.ID == e.ID).Execute().ExecuteNonQuery();
        }
        
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Address;
            if (e == null)
                return -1;

            return Add(e);
        }

        /// <summary>
        /// 获取详细地址一致的地址信息
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Model.Address GetEntity(string address)
        {
            if (string.IsNullOrEmpty(address)) return null;

            return GetEntity<Model.Address>(t => t.Content == address);
        }

        /// <summary>
        /// 模糊匹配地址
        /// </summary>
        /// <param name="chs">
        /// <para>匹配的字符集（以“与”的关系匹配字符集中的字符，顺序一致）</para>
        /// <para>如果匹配字符集为空，那么停止匹配</para>
        /// </param>
        /// <returns></returns>
        private List<Model.Address> Match(params char[] chs)
        {
            if (chs.Length == 0) return new List<Model.Address>();
            var query = SelectHandler.Columns(string.Format("top 10 Pgis_Address.*")).From<Model.Address>();
            var s = string.Empty;
            for (var i = 0; i < chs.Length; i++)
            {
                s = chs[i].ToString();
                //移除空格键
                if (string.IsNullOrWhiteSpace(s)) continue;
                query = query.Where<Model.Address>(t => t.Content.Like(s));
            }
            //分页并获取前10条记录信息

            return ExecuteList<Model.Address>(query.Execute().ExecuteDataReader());
        }

        private List<Model.Address> Match(params string[] patterns)
        {
            if (patterns.Length == 0)
                return new List<Model.Address>();
            
            var query = SelectHandler.Columns(string.Format("top 10 Pgis_Address.*")).From<Model.Address>();
            for (var i = 0; i < patterns.Length; i++)
            {
                var str = patterns[i];
                query = query.Where<Model.Address>(t => t.Content.Like(str));
            }

            return ExecuteList<Model.Address>(query.Execute().ExecuteDataReader());
        }
    }
}

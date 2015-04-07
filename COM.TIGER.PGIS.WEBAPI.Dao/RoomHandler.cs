using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 楼房处理程序
    /// </summary>
    public class RoomHandler:DBase
    {
        //setting singleton instance
        private RoomHandler() { }
        private static RoomHandler _instance;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static RoomHandler Handler
        {
            get { return _instance = _instance ?? new RoomHandler(); }
        }

        /// <summary>
        /// 获取所有的楼房信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Rooms> GetEntities()
        {
            return ExecuteList<Model.Rooms>(SelectHandler.From<Model.Rooms>().Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定大楼ID组的楼房信息
        /// </summary>
        /// <param name="ids">指定大楼的ID组</param>
        /// <returns></returns>
        public List<Model.Rooms> GetEntitiesWithBuilding(params string[] ids)
        {
            ids = ids.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();
            if (ids.Length == 0) return new List<Model.Rooms>();

            var query = SelectHandler.From<Model.Rooms>()
                .Join(JoinType.Inner, "Pgis_Unit").On("Pgis_Rooms.UnitID = Pgis_Unit.Unit_ID")
                .Where<Model.Rooms>(t => t.OwnerInfoID.In(ids))
                .Where<Model.Unit>(t => t.OwnerInfoID.In(ids));
            return ExecuteList<Model.Rooms>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定单元组ID的楼房信息
        /// </summary>
        /// <param name="ids">指定单元组的ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.Rooms> GetEntitiesWithUnit(params string[] ids)
        {
            ids = ids.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();
            if (ids.Length == 0) return new List<Model.Rooms>();

            return GetEntities<Model.Rooms>(t => t.UnitID.In(ids));
        }

        /// <summary>
        /// 分页获取指定大楼的楼房信息，并获取当前页码的楼房信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">指定大楼的总楼房数</param>
        /// <param name="ids">指定大楼的ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.Rooms> PageWithBuilding(int index, int size, out int records, params string[] ids)
        {
            var query = SelectHandler.Columns("Pgis_Rooms.*, ROW_NUMBER() OVER ( order by Pgis_Rooms.Room_ID desc ) AS rownum")
                .From<Model.Rooms>()
                .Join(JoinType.Inner, "Pgis_Unit").On("Pgis_Rooms.UnitID = Pgis_Unit.Unit_ID");

            ids = ids.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();
            if (ids.Length > 0)
            {
                query = query
                    .Where<Model.Rooms>(t => t.OwnerInfoID.In(ids))
                    .Where<Model.Unit>(t => t.OwnerInfoID.In(ids));
            }
            records = Count(query);
            query = SelectHandler.Columns("temp.*").From(string.Format("({0}) as temp", query.Execute().CommandText))
                .Where(string.Format("rownum > {0} and rownum <= {1}", (index - 1) * size, index * size));
            return ExecuteList<Model.Rooms>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定ID的 Model.Room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Rooms GetEntity(string id)
        {
            var e = GetEntity<Model.Rooms>(t => t.Room_ID.In(id));
            e.BuildingInfo = GetEntity<Model.Building>(t => t.OwnerInfoID == e.OwnerInfoID);
            return e;
        }

        /// <summary>
        /// 添加新的房间信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Rooms;
            if (e == null)
                return 0;

            if (null != GetEntity<Model.Rooms>(t => (t.RoomName == e.RoomName || t.RoomName2 == e.RoomName2) && t.UnitID == e.UnitID)) 
                return -2;

            var query = InsertHandler.Into<Model.Rooms>()
                .Table("OwnerInfoID", "RoomArea", "RoomAttribute", "RoomAttributeID", "RoomName", "RoomName2", "RoomUse", "RoomUseID", "UnitID", "UnitName")
                .Values(e.OwnerInfoID, e.RoomArea, e.RoomAttribute, e.RoomAttributeID,e.RoomName, e.RoomName2, e.RoomUse, e.RoomUseID, e.UnitID, e.UnitName);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的房间信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Rooms;
            if (e == null)
                return 0;

            if (null != GetEntity<Model.Rooms>(t => (t.RoomName == e.RoomName || t.RoomName2 == e.RoomName2) && t.UnitID == e.UnitID && t.Room_ID != e.Room_ID))
                return -2;

            var query = UpdateHandler.Table<Model.Rooms>()
                .Set("OwnerInfoID").EqualTo(e.OwnerInfoID)
                .Set("RoomArea").EqualTo(e.RoomArea)
                .Set("RoomAttribute").EqualTo(e.RoomAttribute)
                .Set("RoomAttributeID").EqualTo(e.RoomAttributeID)
                .Set("RoomName").EqualTo(e.RoomName)
                .Set("RoomName2").EqualTo(e.RoomName2)
                .Set("RoomUse").EqualTo(e.RoomUse)
                .Set("RoomUseID").EqualTo(e.RoomUseID)
                .Set("UnitID").EqualTo(e.UnitID)
                .Set("UnitName").EqualTo(e.UnitName)
                .Where<Model.Rooms>(t => t.Room_ID == e.Room_ID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的房间信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;

            return DeleteHandler.From<Model.Rooms>().Where<Model.Rooms>(t => t.Room_ID.In(ids)).Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 获取条目数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int Count(IDao.ISelect query)
        {
            query = SelectHandler.Columns("temp.rownum").From(string.Format("({0}) as temp", query.Execute().CommandText));
            return Convert.ToInt32(query.Execute().ExecuteSaclar());
        }

        /// <summary>
        /// 校验名称，别名重复
        /// <para>大楼有且只有唯一的房间名称和别名</para>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CheckRename(Model.Rooms e)
        {
            return null != GetEntity<Model.Rooms>(t => (t.RoomName == e.RoomName || t.RoomName2 == e.RoomName2) && t.UnitID == e.UnitID);
        }
    }
}

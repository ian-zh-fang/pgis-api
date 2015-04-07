using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 街巷处理程序
    /// </summary>
    public class StreetHandler :DBase
    {
        private static StreetHandler _instance = null;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static StreetHandler Handler { get { return _instance = _instance ?? new StreetHandler(); } }
        //set singleton instance
        private StreetHandler() { }

        /// <summary>
        /// 获取所有的街巷信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Street> GetEntities()
        {
            var query = SelectHandler.From<Model.Street>();
            return ExecuteList<Model.Street>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定行政区划的所有街巷信息
        /// </summary>
        /// <param name="adminids">指定的行政区划ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.Street> GetEntities(params string[] adminids)
        {
            if (adminids.Length == 0) return new List<Model.Street>();

            return GetEntities<Model.Street>(t => t.AdminID.In(adminids));
        }

        /// <summary>
        /// 分页查询所有的街道信息，并获取当前页码的数据记录
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        public List<Model.Street> Page(int index, int size, out int records)
        {
            return Paging<Model.Street>(index, size, null, OrderType.Desc, out records, "Pgis_Street.ID");
        }

        /// <summary>
        /// 获取指定ID的街巷信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Street GetEntity(int id)
        {
            return GetEntity<Model.Street>(t => t.ID == id);
        }

        /// <summary>
        /// 添加新的记录信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Street;
            if (CheckRename(e)) return -2;

            var query = InsertHandler.Into<Model.Street>()
                .Table("AdminID", "AdminName", "Alias", "EndTime", "FirstLetter", "GisGrid", "Lat", "LeftEndNum", "LeftNumTypeID", "LeftNumTypeName", "LeftStartNum", "LivingState", "Lng", "Name", "Position", "PositionID", "RightEndNum", "RightStartNum", "SourceFrom", "X", "Y")
                .Values(e.AdminID, e.AdminName, e.Alias, e.EndTime == DateTime.MinValue? null: e.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.FirstLetter, e.GisGrid, e.Lat, e.LeftEndNum, e.LeftNumTypeID, e.LeftNumTypeName, e.LeftStartNum, e.LivingState, e.Lng, e.Name, e.Position, e.PositionID, e.RightEndNum, e.RightStartNum, e.SourceFrom, e.X, e.Y);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的记录信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Street;
            if (CheckRename(e)) return -2;

            var query = UpdateHandler.Table<Model.Street>()
                .Set("AdminID").EqualTo(e.AdminID)
                .Set("AdminName").EqualTo(e.AdminName)
                .Set("Alias").EqualTo(e.Alias)
                .Set("EndTime").EqualTo(e.EndTime == DateTime.MinValue ? null : e.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Set("FirstLetter").EqualTo(e.FirstLetter)
                .Set("GisGrid").EqualTo(e.GisGrid)
                .Set("Lat").EqualTo(e.Lat)
                .Set("LeftEndNum").EqualTo(e.LeftEndNum)
                .Set("LeftNumTypeID").EqualTo(e.LeftNumTypeID)
                .Set("LeftNumTypeName").EqualTo(e.LeftNumTypeName)
                .Set("LeftStartNum").EqualTo(e.LeftStartNum)
                .Set("LivingState").EqualTo(e.LivingState)
                .Set("Lng").EqualTo(e.Lng)
                .Set("Name").EqualTo(e.Name)
                .Set("Position").EqualTo(e.Position)
                .Set("PositionID").EqualTo(e.PositionID)
                .Set("RightEndNum").EqualTo(e.RightEndNum)
                .Set("RightStartNum").EqualTo(e.RightStartNum)
                .Set("SourceFrom").EqualTo(e.SourceFrom)
                .Set("X").EqualTo(e.X)
                .Set("Y").EqualTo(e.Y)
                .Where<Model.Street>(t => t.ID == e.ID);

            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的记录信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;
            return DeleteHandler.From<Model.Street>().Where<Model.Street>(t => t.ID.In(ids))
                .Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 校验重复的名称，同一个行政区有且只有一个同样的街巷
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CheckRename(Model.Street e)
        {
            var obj = GetEntity<Model.Street>(t => t.Name == e.Name && t.AdminID == e.AdminID);
            return obj != null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 大楼单元处理程序
    /// </summary>
    public class UnitHandler:DBase
    {
        //set singleton instance
        private UnitHandler() { }
        private static UnitHandler _instance;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static UnitHandler Handler 
        {
            get { return _instance = _instance ?? new UnitHandler(); }
        }

        /// <summary>
        /// 获取所有的大楼单元组信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Unit> GetEntities()
        {
            var query = SelectHandler.From<Model.Unit>();
            return ExecuteList<Model.Unit>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定大楼的单元信息
        /// </summary>
        /// <param name="buildingids">指定大楼的ID组，以“，”分隔</param>
        /// <returns></returns>
        public List<Model.Unit> GetEntities(params string[] buildingids)
        {
            if (buildingids.Length == 0) return new List<Model.Unit>();

            return GetEntities<Model.Unit>(t => t.OwnerInfoID.In(buildingids));
        }

        /// <summary>
        /// 分页所有的大楼单元组信息，并获取当前页码的单元组信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">所有的条目数</param>
        /// <returns></returns>
        public List<Model.Unit> Page(int index, int size, out int records)
        {
            return Paging<Model.Unit>(index, size, null, OrderType.Desc, out records, "Pgis_Unit.Unit_ID");
        }

        /// <summary>
        /// 添加新的单元组信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Unit;
            if (e == null) return 0;

            if (null != GetEntity<Model.Unit>(t => t.UnitName == e.UnitName && t.OwnerInfoID == e.OwnerInfoID))
                return -2;

            var query = InsertHandler.Into<Model.Unit>()
                .Table("UnitName", "Sort", "OwnerInfoID")
                .Values(e.UnitName, e.Sort, e.OwnerInfoID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定的单元组信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Unit;
            if (e == null) return 0;

            if (null != GetEntity<Model.Unit>(t => t.UnitName == e.UnitName && t.OwnerInfoID == e.OwnerInfoID && t.Unit_ID != e.Unit_ID))
                return -2;

            var query = UpdateHandler.Table<Model.Unit>()
                .Set("OwnerInfoID").EqualTo(e.OwnerInfoID)
                .Set("Sort").EqualTo(e.Sort)
                .Set("UnitName").EqualTo(e.UnitName)
                .Where<Model.Unit>(t => t.Unit_ID == e.Unit_ID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的单元组信息
        /// </summary>
        /// <param name="ids">指定的ID组，以“，”分隔</param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;

            return DeleteHandler.From<Model.Unit>().Where<Model.Unit>(t => t.Unit_ID.In(ids)).Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 校验重命名问题，每一幢大楼的每一个单元必须是唯一的
        /// </summary>
        /// <param name="e">需要校验的单元组信息</param>
        /// <returns></returns>
        private bool CheckRename(Model.Unit e)
        {
            var x = GetEntity<Model.Unit>(t => t.OwnerInfoID == e.OwnerInfoID && t.UnitName == e.UnitName && t.Unit_ID != e.Unit_ID);
            return null != x;
        }
    }
}

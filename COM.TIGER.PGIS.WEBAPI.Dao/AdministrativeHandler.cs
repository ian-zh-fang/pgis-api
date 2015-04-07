using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 行政区划处理程序
    /// </summary>
    public class AdministrativeHandler:DBase
    {
        private static AdministrativeHandler _instance = null;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static AdministrativeHandler Handler
        {
            get { return _instance = _instance ?? new AdministrativeHandler(); }
        }
        //get singleton instance
        private AdministrativeHandler() { }

        /// <summary>
        /// 获取所有的行政区划信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Administrative> GetEntities()
        {
            var query = SelectHandler.From<Model.Administrative>();
            return ExecuteList<Model.Administrative>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取顶级行政区划信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Administrative> GetTopEntities()
        {
            return GetEntities<Model.Administrative>(t => t.PID == 0 || t.PID == null);
        }

        /// <summary>
        /// 获取指定行政区划的下一级行政区划信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.Administrative> GetEntities(int id)
        {
            return GetEntities<Model.Administrative>(t => t.PID == id);
        }

        /// <summary>
        /// 获取所有的行政区划信息，以树形式返回
        /// </summary>
        /// <returns></returns>
        public List<Model.Administrative> GetEntitiesTree()
        {
            var items = GetEntities();
            var tops = items.Where(t => t.PID == 0 || t.PID == null).ToList();
            tops.ForEach(t => t.AddRange(items));
            return tops;
        }

        /// <summary>
        /// 分页所有的实体信息
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <returns></returns>
        public List<Model.Administrative> Page(int index, int size, out int records)
        {
            return base.Paging<Model.Administrative>(index, size, null, OrderType.Desc, out records, "Pgis_Administrative.ID");
        }

        /// <summary>
        /// 分页指定行政区划的下一级行政区划信息，并获取当前页码的详细行政区划信息，返回一个行政区划信息列表
        /// </summary>
        /// <param name="id">指定的行政区划信息</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">指定行政区划的下一级行政区划总数量</param>
        /// <returns></returns>
        public List<Model.Administrative> PageSub(int id, int index, int size, out int records)
        {
            return Paging<Model.Administrative>(index, size, t => t.PID == id, OrderType.Desc, out records, "Pgis_Administrative.ID");
        }

        /// <summary>
        /// 分页顶级行政区划信息，并获取当前页码的行政区划详细信息，返回一个行政区划的详细信息列表
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">顶级行政区划总数量</param>
        /// <returns></returns>
        public List<Model.Administrative> PageTop(int index, int size, out int records)
        {
            return Paging<Model.Administrative>(index, size, t => t.PID == 0 || t.PID == null, OrderType.Desc, out records, "Pgis_Administrative.ID");
        }

        /// <summary>
        /// 获取指定ID的实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Administrative GetEntity(int id)
        {
            return GetEntity<Model.Administrative>(t => t.ID == id);
        }

        /// <summary>
        /// 添加新的行政区划
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int InsertEntity(object obj)
        {
            var e = obj as Model.Administrative;
            if (CheckReName(e)) return -2;

            var query = InsertHandler.Into<Model.Administrative>()
                .Table("AreaID", "AreaName", "Name", "PID", "Code")
                .Values(e.AreaID, e.AreaName, e.Name, e.PID, e.Code)
                .InsertValue("FirstLetter", string.Format("dbo.fun_getPYFirst('{0}')", e.Name));
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新行政区划信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int UpdateEntity(object obj)
        {
            var e = obj as Model.Administrative;
            if (CheckReName(e)) return -2;

            var query = UpdateHandler.Table<Model.Administrative>()
                .Set("PID").EqualTo(e.PID)
                .Set("Name").EqualTo(e.Name)
                .Set("FirstLetter").EqualTo(string.Format("dbo.fun_getPYFirst('{0}')", e.Name), true)
                .Set("AreaName").EqualTo(e.AreaName)
                .Set("AreaID").EqualTo(e.AreaID)
                .Set("Code").EqualTo(e.Code)
                .Where<Model.Administrative>(t => t.ID == e.ID);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的实体信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            if (ids.Length == 0) return 0;

            var query = DeleteHandler.From<Model.Administrative>()
                .Where<Model.Administrative>(t => t.ID.In(ids));
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 校验重复的名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool CheckReName(Model.Administrative entity)
        {
            var e = GetEntity<Model.Administrative>(t => (t.Name == entity.Name || t.Code == entity.Code) && t.ID != entity.ID);
            return e != null;
        }
    }
}

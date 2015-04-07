using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class BelongToHandler:DBase
    {
        private static BelongToHandler _handler = null;
        public static BelongToHandler Handler { 
            get { return _handler = _handler ?? new BelongToHandler(); } }

        /// <summary>
        /// 获取指定ID的记录信息
        /// <para>如果没有指定，获取所有的记录信息</para>
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Model.BelongTo> GetEntities(params string[] ids)
        {
            if (ids.Length == 0)
            {
                var query = SelectHandler.From<Model.BelongTo>();
                return ExecuteList<Model.BelongTo>(query.Execute().ExecuteDataReader());
            }

            return GetEntities<Model.BelongTo>(t => t.ID.In(ids));
        }

        /// <summary>
        /// 获取指定ID的记录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.BelongTo GetEntity(int id)
        {
            return GetEntity<Model.BelongTo>(t => t.ID == id);
        }

        /// <summary>
        /// 添加新的记录信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertEntity(Model.BelongTo e)
        {
            var query = InsertHandler.Into<Model.BelongTo>()
                .Table("Code", "Description", "Name")
                .Values(e.Code, e.Description, e.Name);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 更新指定记录信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int UpdateEntity(Model.BelongTo e)
        {
            var query = UpdateHandler.Table<Model.BelongTo>()
                .Set("Code").EqualTo(e.Code)
                .Set("Description").EqualTo(e.Description)
                .Set("Name").EqualTo(e.Name);
            var handler = query.IQueryWhere.Where<Model.BelongTo>(t => t.ID == e.ID).Where<IDao.IUpdate>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 批量移除指定ID的记录信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            var query = DeleteHandler.From<Model.BelongTo>();
            var handler = query.IQueryWhere.Where<Model.BelongTo>(t => t.ID.In(ids)).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">每页条目</param>
        /// <param name="records">总记录数量</param>
        /// <returns></returns>
        public List<Model.BelongTo> Paging(int index, int size, out int records)
        {
            return Paging<Model.BelongTo>(index, size, null, OrderType.Desc, out records, "pgis_belongto.id");
        }
    }
}

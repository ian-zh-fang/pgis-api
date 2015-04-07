using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 单位标注处理程序
    /// </summary>
    public class CompanyMarkHandler:DBase
    {
        private static CompanyMarkHandler _handler = null;

        public static CompanyMarkHandler Handler
        {
            get { return _handler = _handler ?? new CompanyMarkHandler(); }
        }
        private CompanyMarkHandler() { }

        /// <summary>
        /// 分页获取所有的单位标注数据，并获取指定页码的数据
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="size">每页记录条目数</param>
        /// <param name="records">记录条目总数</param>
        /// <returns></returns>
        public List<Model.CompanyMark> Paging(int index, int size, out int records)
        {
            return Paging<Model.CompanyMark>(index, size, null, OrderType.Desc, out records, "pgis_companymark.id");
        }

        public List<Model.CompanyMark> Paging(int index, int size, int type, out int records)
        {
            return Paging<Model.CompanyMark>(index, size, t => t.Type == type, OrderType.Desc, out records, "pgis_companymark.id");
        }

        public List<Model.CompanyMark> Paging(int index, int size, out int records, params string[] types)
        {
            return Paging<Model.CompanyMark>(index, size, t => t.Type.In(types), OrderType.Desc, out records, "pgis_companymark.id");
        }
        

        /// <summary>
        /// 添加新的单位标注数据记录
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int InsertEntity(Model.CompanyMark e)
        {
            var query = InsertHandler.Into<Model.CompanyMark>()
                .Table("Description", "Name", "Telephone", "X", "Y","Type")
                .Values(e.Description, e.Name, e.Telephone, e.X, e.Y,e.Type);
            return query.Execute().ExecuteNonQuery();
        }

        /// <summary>
        /// 移除指定ID的单位标注数据记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override int DeleteEntities(params string[] ids)
        {
            var query = DeleteHandler.From<Model.CompanyMark>();
            var handler = query.IQueryWhere.Where<Model.CompanyMark>(t => t.ID.In(ids)).Where<IDao.IDelete>(query).Execute();
            return handler.ExecuteNonQuery();
        }
    }
}

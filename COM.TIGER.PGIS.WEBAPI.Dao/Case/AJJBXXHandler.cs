using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public class AJJBXXHandler:DBase
    {
        /*
         * 业务说明：
         *  1，依据案件编号，涉案人员姓名，涉案人员编号，案件分类（是否吸毒，是否网上追逃，是否刑拘）分页模糊查询所有涉案人员名单；
         *  2，分类统计全县案件总数（分类包括：是否吸毒，是否网上追逃，是否刑拘）；
         *  3，查询同一案件的所有涉案人员
         */

        protected AJJBXXHandler() { }
        private static AJJBXXHandler _instance = null;
        public static AJJBXXHandler Handler { get { return _instance = _instance ?? new AJJBXXHandler(); } }

        /// <summary>
        /// 依据案件编号，涉案人员姓名，涉案人员公民身份证号，案件分类（是否吸毒，是否网上追逃，是否刑拘）分页模糊查询所有涉案人员
        /// </summary>
        /// <param name="bh">案件编号</param>
        /// <param name="xm">涉案人员姓名</param>
        /// <param name="cnb">涉案人员公民身份证号</param>
        /// <param name="isdrup">是否吸毒</param>
        /// <param name="ispursuit">是否网上追逃</param>
        /// <param name="isarrest">是否刑拘</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">总案件数</param>
        /// <returns>当前页码的案件清单</returns>
        public List<Model.AJJBXX> Query(string bh, string xm, string cnb, int isdrup, int ispursuit, int isarrest, int index, int size, out int records)
        {
            var query = GetPageQuery(GetTableName<Model.AJJBXX>(), "ID")
                .Where<Model.AJJBXX>(t => t.IsArrest == isarrest && t.IsDrup == isdrup && t.IsPursuit == ispursuit);
            
            if (!string.IsNullOrWhiteSpace(bh))
            {
                query = query.Where<Model.AJJBXX>(t => t.Ajbh.Like(bh));
            }

            if (!string.IsNullOrWhiteSpace(xm))
            {
                query = query.Where<Model.AJJBXX>(t => t.Xm.Like(xm));
            }

            if (!string.IsNullOrWhiteSpace(cnb))
            {
                query = query.Where<Model.AJJBXX>(t => t.CardNo.Like(cnb));
            }

            return Paging<Model.AJJBXX>(query, index, size, out records);
        }

        /// <summary>
        /// 查询同一案件的所有涉案人员
        /// </summary>
        /// <param name="bh">案件编号</param>
        /// <returns></returns>
        public List<Model.AJJBXX> QueryByBH(string bh)
        {
            if (string.IsNullOrWhiteSpace(bh))
                return new List<Model.AJJBXX>();

            var query = SelectHandler.From<Model.AJJBXX>()
                .Where<Model.AJJBXX>(t => t.Ajbh == bh);

            return ExecuteList<Model.AJJBXX>(query.Execute().ExecuteDataReader());
        }

        /*
         * 案件统计业务逻辑：
         *  1，应该依据案件编号统计各项案件信息
         *  2，应该分类统计案件信息（分类包括：吸毒，网上追逃，刑拘）
         */

        /// <summary>
        /// 分类统计案件信息
        /// eg:
        /// <para>    1，吸毒，0</para>
        /// <para>    2，网上追逃，0</para>
        /// <para>    3，刑拘，0</para>
        /// <para>    4，其它，0</para>
        /// <para>    5，总数，0</para>
        /// </summary>
        /// <returns></returns>
        public List<Model.CountCase> TotalCase()
        {
            var str = @"select COUNT(distinct AJBH) as Records, '吸毒' as Name
from tongzi_new.dbo.Pgis_AJJBXX
where ISDRUP >= 1
union all
select COUNT(distinct AJBH) as Records, '网上追逃' as Name
from tongzi_new.dbo.Pgis_AJJBXX
where ISPURSUIT >= 1
union all
select COUNT(distinct AJBH) as Records, '刑拘' as Name
from tongzi_new.dbo.Pgis_AJJBXX
where ISARREST >= 1
union all
select COUNT(distinct AJBH) as Records, '其它' as Name
from tongzi_new.dbo.Pgis_AJJBXX
where (ISARREST = 0) and  (ISDRUP = 0) and  (ISPURSUIT = 0)
union all
select COUNT(distinct AJBH) as Records, '总数' as Name
from tongzi_new.dbo.Pgis_AJJBXX";

            return ExecuteList<Model.CountCase>((new IDao.Dbase()).ExecuteDataReader(str));
        }
    }
}

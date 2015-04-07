using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    public abstract class CaseHandler<T>:DBase where T:new()
    {
        private string _tablename = null;
        /// <summary>
        /// 获取实体对应数据表名称
        /// </summary>
        public string TableName 
        {
            get { return _tablename = _tablename ?? GetTableName<T>(); }
        }

        /// <summary>
        /// 统计各项案件分类信息
        /// </summary>
        /// <returns></returns>
        public virtual List<Model.Case> TotalByKinds()
        {
            return TotalWithKinds();
        }

        /// <summary>
        /// 统计指定行政区各项案件划分类信息
        /// </summary>
        /// <param name="adminid">指定行政区划ID</param>
        /// <returns></returns>
        public virtual List<Model.Case> TotalByKinds(int adminid)
        {
            return TotalWithKinds(adminid);
        }

        /// <summary>
        /// 分类统计当前警报记录
        /// <para>eg:</para>
        /// <para>当日警报记录：</para>
        /// <para>本周警报记录：</para>
        /// <para>当月警报记录：</para>
        /// <para>当年警报记录：</para>
        /// </summary>
        /// <returns></returns>
        protected List<Model.Case> TotalWithKinds(params string[] conditions)
        {
            var columns = new string[] 
            {
                "TypeID,TypeName",
                string.Format("({0}) as TodayTickCount", TotalQueryWithToday(conditions).CommandText),
                string.Format("({0}) as ThisWeekTickCount", TotalQueryWithThisWeek(conditions).CommandText),
                string.Format("({0}) as ThisMonthTickCount", TotalQueryWithThisMonth(conditions).CommandText),
                string.Format("({0}) as ThisYearTickCount", TotalQueryWithThisYear(conditions).CommandText),
            };
            var query = SelectHandler.Columns(columns).From(string.Format("{0} as t", TableName)).GroupBy("TypeID,TypeName");
            return ExecuteList<Model.Case>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取指定行政区划的案件统计信息
        /// </summary>
        /// <param name="adminid">行政区划ID</param>
        /// <returns></returns>
        protected List<Model.Case> TotalWithKinds(int adminid)
        {
            var condition = string.Format("{0}.AdminID = {1}", TableName, adminid);
            return TotalWithKinds(condition);
        }

        /// <summary>
        /// 当天统计查询
        /// </summary>
        /// <returns></returns>
        protected IDao.ISelect TotalQueryWithToday(params string[] conditions)
        {
            var timestart = DateTime.Now.Date;
            return TotalQueryWith(timestart, conditions);
        }

        /// <summary>
        /// 本周统计查询
        /// </summary>
        /// <returns></returns>
        protected IDao.ISelect TotalQueryWithThisWeek(params string[] conditions)
        {
            var d = 0 - (int)DateTime.Now.DayOfWeek;
            var timestart = DateTime.Today.AddDays(d);
            return TotalQueryWith(timestart, conditions);
        }

        /// <summary>
        /// 当月统计查询
        /// </summary>
        /// <returns></returns>
        protected IDao.ISelect TotalQueryWithThisMonth(params string[] conditions)
        {
            var d = 0 - ((int)DateTime.Now.Day - 1);
            var timestart = DateTime.Today.AddDays(d);
            return TotalQueryWith(timestart, conditions);
        }

        /// <summary>
        /// 当年统计查询
        /// </summary>
        /// <returns></returns>
        protected IDao.ISelect TotalQueryWithThisYear(params string[] conditions)
        {
            var d = 0 - ((int)DateTime.Now.DayOfYear - 1);
            var timestart = DateTime.Today.AddDays(d);
            return TotalQueryWith(timestart, conditions);
        }      

        /// <summary>
        /// 匹配查询条件
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        protected IDao.ISelect TotalQueryWith( DateTime? timestart, params string[] conditions)
        {
            var query = SelectHandler.Columns("count(0)").From<T>()
                .Where(string.Format("{0}.TypeID = t.TypeID", TableName));
            //匹配查询时间区间
            MatchDateTimeArea(timestart, DateTime.Now, ref query);
            //其他查询条件
            for (var i = 0; i < conditions.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(conditions[i])) continue;
                query = query.Where(conditions[i]);
            }
            //分组
            return query.GroupBy(string.Format("{0}.TypeID", TableName));
        }

        /// <summary>
        /// 模糊匹配地址
        /// </summary>
        /// <param name="address"></param>
        /// <param name="query"></param>
        protected new void MatchAddress(string address, ref IDao.ISelect query)
        {
            if (string.IsNullOrWhiteSpace(address)) return;

            var arr = address.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < arr.Length; i++)
            {
                query = query.Where(string.Format("{0}.Location like '%{1}%'", TableName, arr[i]));
            }
        }

        /// <summary>
        /// 匹配报警时间区间
        /// </summary>
        /// <param name="timestart">区间开始。NULL标识无开始时间</param>
        /// <param name="timeend">区间结束。NULL标识无结束时间</param>
        /// <param name="query"></param>
        protected void MatchDateTimeArea(DateTime? timestart, DateTime? timeend, ref IDao.ISelect query)
        {
            if (timestart != null)
            {
                query = query.Where(string.Format("{1}.AlarmTime >= '{0}'", ((DateTime)timestart).ToString("yyyy-MM-dd HH:mm:ss:fff"), TableName));
            }

            if (timeend != null)
            {
                query = query.Where(string.Format("{1}.AlarmTime <= '{0}'", ((DateTime)timeend).ToString("yyyy-MM-dd HH:mm:ss:fff"), TableName));
            }
        }
    }
}

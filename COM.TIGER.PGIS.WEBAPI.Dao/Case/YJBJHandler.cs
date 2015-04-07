using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 一件报警
    /// </summary>
    public class YJBJHandler : CaseHandler<Model.YJBJ>
    {
        //setting singleton instance
        private YJBJHandler() { }
        private static YJBJHandler _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static YJBJHandler Handler
        {
            get { return _instance = _instance ?? new YJBJHandler(); }
        }

        /// <summary>
        /// 获取所有的报警记录信息
        /// </summary>
        /// <returns></returns>
        public List<Model.YJBJ> GetEntities()
        {
            var query = SelectHandler.From<Model.YJBJ>();
            return ExecuteList<Model.YJBJ>(query.Execute().ExecuteDataReader());
        }

        /// <summary>
        /// 获取与地址模糊匹配的所有信息
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public List<Model.YJBJ> GetEntities(string addr)
        {
            if (string.IsNullOrWhiteSpace(addr)) {
                return new List<Model.YJBJ>();
            }
            var query = SelectHandler.From<Model.YJBJ>();
            MatchAddress(addr, ref query);
            var data = ExecuteList<Model.YJBJ>(query.Execute().ExecuteDataReader());
            data = data.DistinctBy(t => t.Location).ToList();
            return data;
        }

        /// <summary>
        /// 分页所有的报警记录信息，并获取当前页码的报警记录信息
        /// </summary>
        /// <param name="inde">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">总条目数</param>
        /// <returns></returns>
        public List<Model.YJBJ> Page(int inde, int size, out int records)
        {
            return Paging<Model.YJBJ>(inde, size, null, OrderType.Desc, out records, string.Format("{0}.ID", TableName));
        }

        /// <summary>
        /// 获取指定时间开始，到目前为止的所有案件信息
        /// </summary>
        /// <param name="timestart">指定的案件开始时间</param>
        /// <returns></returns>
        public List<Model.YJBJ> DistributedQuery(DateTime timestart)
        {
            return GetEntities<Model.YJBJ>(t => t.AlarmTime >= timestart && t.AlarmTime <= DateTime.Now);
        }

        /// <summary>
        /// 获取指定的警报信息
        /// </summary>
        /// <param name="id">指定的警报ID</param>
        /// <returns></returns>
        public Model.YJBJ GetEntity(int id)
        {
            return GetEntity<Model.YJBJ>(t => t.ID == id);
        }

        /*******************************************************************
         *  综合查询
         * *****************************************************************
         */

        /// <summary>
        /// 分页查询指定记录信息，并获取当前页码的记录信息
        /// </summary>
        /// <param name="alarmname">报警人姓名</param>
        /// <param name="alarmtel">报警电话</param>
        /// <param name="alarmaddress">报警地址</param>
        /// <param name="timestart">查询警报区间开始</param>
        /// <param name="timeend">查询警报区间结束</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">当前查询纵条目数</param>
        /// <returns></returns>
        public List<Model.YJBJ> Page(string alarmnum, string alarmname, string alarmtel, string alarmaddress,
            DateTime? timestart, DateTime? timeend,
            int index, int size, out int records)
        {
            var query = GetPageQuery(TableName, "ID");
            if (!string.IsNullOrWhiteSpace(alarmnum))
            {
                query = query.Where<Model.YJBJ>(t => t.Num.Like(alarmnum));
            }
            //匹配报警人姓名
            if (!string.IsNullOrWhiteSpace(alarmname))
            {
                query = query.Where<Model.YJBJ>(t => t.AlarmMan.Like(alarmname));
            }
            //匹配报警电话
            if (!string.IsNullOrWhiteSpace(alarmtel))
            {
                query = query.Where<Model.YJBJ>(t => t.Tel.Like( alarmtel));
            }
            //模糊匹配地址
            MatchAddress(alarmaddress, ref query);
            //匹配报警时间区间
            MatchDateTimeArea(timestart, timeend, ref query);
            //执行命令
            return Paging<Model.YJBJ>(query, index, size, out records);
        }

        public Model.Case JDJTotalCaseOn()
        {
            var sqlstr = @"select * from 
(
	select id, 'TodayTickCount' as col from Pgis_JCJ_JJDB where DateDiff(DD,AlarmTime,getdate())=0 
	union all
	select id, 'ThisWeekTickCount' as col from Pgis_JCJ_JJDB where DateDiff(WEEK,AlarmTime,getdate())=0
	union all
	select id, 'ThisMonthTickCount' as col from Pgis_JCJ_JJDB where DateDiff(MONTH,AlarmTime,getdate())=0
	union all
	select id, 'ThisYearTickCount' as col from Pgis_JCJ_JJDB where DateDiff(YEAR,AlarmTime,getdate())=0
) t
pivot (count(id) for col in([TodayTickCount], [ThisWeekTickCount], [ThisMonthTickCount], [ThisYearTickCount])) tt";

            return ExecuteEntity<Model.Case>(new IDao.Dbase().ExecuteDataReader(sqlstr));
        }

        public List<Model.Case> JDdTotalCasesOnArea()
        {
            var sqlstr = @"select * from 
(
	select c.id as TypeID, c.name as TypeName, 'TodayTickCount' as col from Pgis_JCJ_JJDB a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(DD,AlarmTime,getdate())=0 
	union all
	select c.id as TypeID, c.name as TypeName, 'ThisWeekTickCount' as col from Pgis_JCJ_JJDB  a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(WEEK,AlarmTime,getdate())=0
	union all
	select c.id as TypeID, c.name as TypeName, 'ThisMonthTickCount' as col from Pgis_JCJ_JJDB  a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(MONTH,AlarmTime,getdate())=0
	union all
	select c.id as TypeID, c.name as TypeName, 'ThisYearTickCount' as col from Pgis_JCJ_JJDB  a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(YEAR,AlarmTime,getdate())=0
) t
pivot (count(col) for col in([TodayTickCount], [ThisWeekTickCount], [ThisMonthTickCount], [ThisYearTickCount])) tt";

            return ExecuteList<Model.Case>((new IDao.Dbase()).ExecuteDataReader(sqlstr));
        }

        public Model.Case YJTotalCaseOn()
        {
            var sqlstr = @"select * from 
(
	select id, 'TodayTickCount' as col from Pgis_YJBJ where DateDiff(DD,AlarmTime,getdate())=0 
	union all
	select id, 'ThisWeekTickCount' as col from Pgis_YJBJ where DateDiff(WEEK,AlarmTime,getdate())=0
	union all
	select id, 'ThisMonthTickCount' as col from Pgis_YJBJ where DateDiff(MONTH,AlarmTime,getdate())=0
	union all
	select id, 'ThisYearTickCount' as col from Pgis_YJBJ where DateDiff(YEAR,AlarmTime,getdate())=0
) t
pivot (count(id) for col in([TodayTickCount], [ThisWeekTickCount], [ThisMonthTickCount], [ThisYearTickCount])) tt";

            return ExecuteEntity<Model.Case>(new IDao.Dbase().ExecuteDataReader(sqlstr));
        }

        public List<Model.Case> YJTotalCasesOnArea()
        {
            var sqlstr = @"select * from 
(
	select c.id as TypeID, c.name as TypeName, 'TodayTickCount' as col from Pgis_YJBJ a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(DD,AlarmTime,getdate())=0 
	union all
	select c.id as TypeID, c.name as TypeName, 'ThisWeekTickCount' as col from Pgis_YJBJ  a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(WEEK,AlarmTime,getdate())=0
	union all
	select c.id as TypeID, c.name as TypeName, 'ThisMonthTickCount' as col from Pgis_YJBJ  a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(MONTH,AlarmTime,getdate())=0
	union all
	select c.id as TypeID, c.name as TypeName, 'ThisYearTickCount' as col from Pgis_YJBJ  a 
	left join Pgis_Administrative b on b.ID = a.AdminID
	left join PGis_Area c on c.id = b.AreaID
	where DateDiff(YEAR,AlarmTime,getdate())=0
) t
pivot (count(col) for col in([TodayTickCount], [ThisWeekTickCount], [ThisMonthTickCount], [ThisYearTickCount])) tt";

            return ExecuteList<Model.Case>((new IDao.Dbase()).ExecuteDataReader(sqlstr));
        }
    }
}

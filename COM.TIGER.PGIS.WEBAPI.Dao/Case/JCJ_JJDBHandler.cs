using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Dao
{
    /// <summary>
    /// 三台合一报警
    /// </summary>
    public class JCJ_JJDBHandler : CaseHandler<Model.JCJ_JJDB>
    {
        //setting singleton instance
        private JCJ_JJDBHandler() { }
        private static JCJ_JJDBHandler _instance;
        /// <summary>
        /// getting singleton instance
        /// </summary>
        public static JCJ_JJDBHandler Handler
        {
            get { return _instance = _instance ?? new JCJ_JJDBHandler(); }
        }

        /// <summary>
        /// 获取所有的警报记录
        /// </summary>
        /// <returns></returns>
        public List<Model.JCJ_JJDB> GetEntities()
        {
            var query = SelectHandler.From<Model.JCJ_JJDB>();
            return ExecuteList<Model.JCJ_JJDB>(query.Execute().ExecuteDataReader());
        }

        public List<Model.JCJ_JJDB> GetEntities(string addr)
        {
            if (string.IsNullOrWhiteSpace(addr))
            {
                return new List<Model.JCJ_JJDB>();
            }

            var query = SelectHandler.From<Model.JCJ_JJDB>();
            MatchAddress(addr,ref query);
            var data = ExecuteList<Model.JCJ_JJDB>(query.Execute().ExecuteDataReader());
            data = data.DistinctBy(t => t.Location).ToList();
            return data;
        }

        /// <summary>
        /// 分页所有的警报信息，并获取当前页码的警报记录
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <param name="size">每页条目数</param>
        /// <param name="records">总记录数</param>
        /// <returns></returns>
        public List<Model.JCJ_JJDB> Page(int index, int size, out int records)
        {
            return Paging<Model.JCJ_JJDB>(index, size, null, OrderType.Desc, out records, "Pgis_JCJ_JJDB.ID");
        }

        /// <summary>
        /// 获取指定ID的警报记录
        /// </summary>
        /// <param name="id">指定的警报记录ID值</param>
        /// <returns></returns>
        public Model.JCJ_JJDB GetEntity(int id)
        {
            return GetEntity<Model.JCJ_JJDB>(t => t.ID == id);
        }

        public List<Model.JCJ_JJDB> DistributedQuery(DateTime time)
        {
            return GetEntities<Model.JCJ_JJDB>(t => t.AlarmTime >= time && t.AlarmTime <= DateTime.Now);
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
        public List<Model.JCJ_JJDB> Page(string alarmnum, string alarmname, string alarmtel, string alarmaddress, 
            DateTime? timestart, DateTime? timeend,
            int index, int size, out int records)
        {
            var query = GetPageQuery(TableName, "ID");
            if (!string.IsNullOrWhiteSpace(alarmnum))
            {
                query = query.Where<Model.JCJ_JJDB>(t => t.Num.Like(alarmnum));
            }
            //匹配报警人姓名
            if (!string.IsNullOrWhiteSpace(alarmname))
            {
                query = query.Where<Model.JCJ_JJDB>(t => t.AlarmMan.Like(alarmname));
            }
            //匹配报警电话
            if (!string.IsNullOrWhiteSpace(alarmtel))
            {
                query = query.Where<Model.JCJ_JJDB>(t => t.Tel.Like(alarmtel));
            }
            //模糊匹配地址
            MatchAddress(alarmaddress, ref query);
            //匹配报警时间区间
            MatchDateTimeArea(timestart, timeend, ref query);
            //执行命令
            return Paging<Model.JCJ_JJDB>(query, index, size, out records);
        }
    }
}

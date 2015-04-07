using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class JCJ_JJDBController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> Page(string alarmnum, string alarmname, string alarmtel, string alarmaddress,
            DateTime? timestart, DateTime? timeend,
            int index, int size)
        {
            var records = 0;
            var data = Dao.JCJ_JJDBHandler.Handler.Page(alarmnum, alarmname, alarmtel, alarmaddress,
                timestart, timeend,
                index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Case>> TotalCase(int adminid)
        {
            var data = Dao.JCJ_JJDBHandler.Handler.TotalByKinds(adminid);
            return ResultOk<List<Model.Case>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.JCJ_JJDB>> MatchAddress(string addr)
        {
            var data = Dao.JCJ_JJDBHandler.Handler.GetEntities(addr);
            return ResultOk<List<Model.JCJ_JJDB>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.JCJ_JJDB>> DistributedQuery(DateTime timestart)
        {
            var data = Dao.JCJ_JJDBHandler.Handler.DistributedQuery(timestart);
            return ResultOk<List<Model.JCJ_JJDB>>(data);
        }
    }
}

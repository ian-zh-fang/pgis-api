using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class YJBJController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> Page(string alarmnum, string alarmname, string alarmtel, string alarmaddress,
            DateTime? timestart, DateTime? timeend,
            int index, int size)
        {
            var records = 0;
            var data = Dao.YJBJHandler.Handler.Page(alarmnum, alarmname, alarmtel, alarmaddress, 
                timestart, timeend, 
                index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.YJBJ>> DistributedQuery(DateTime timestart)
        {
            var data = Dao.YJBJHandler.Handler.DistributedQuery(timestart);
            return ResultOk<List<Model.YJBJ>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.YJBJ>> MatchAddress(string addr)
        {
            var data = Dao.YJBJHandler.Handler.GetEntities(addr);
            return ResultOk<List<Model.YJBJ>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Case>> TotalCase(int adminid)
        {
            var data = Dao.YJBJHandler.Handler.TotalByKinds(adminid);
            return ResultOk<List<Model.Case>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Case> JDJTotalCaseOn()
        {
            var data = Dao.YJBJHandler.Handler.JDJTotalCaseOn();
            return ResultOk<Model.Case>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Case>> JDdTotalCasesOnArea()
        {
            var data = Dao.YJBJHandler.Handler.JDdTotalCasesOnArea();
            return ResultOk<List<Model.Case>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Case> YJTotalCaseOn()
        {
            var data = Dao.YJBJHandler.Handler.YJTotalCaseOn();
            return ResultOk<Model.Case>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Case>> YJTotalCasesOnArea()
        {
            var data = Dao.YJBJHandler.Handler.YJTotalCasesOnArea();
            return ResultOk<List<Model.Case>>(data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class AJJBXXController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> Query(string bh, string xm, string cnb, int isdrup, int ispursuit, int isarrest, int index, int size)
        {
            int records = 0;
            var data = Dao.AJJBXXHandler.Handler.Query(bh, xm, cnb, isdrup, ispursuit, isarrest, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.AJJBXX>> QueryByBH(string bh)
        {
            var data = Dao.AJJBXXHandler.Handler.QueryByBH(bh);
            return ResultOk<List<Model.AJJBXX>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CountCase>> TotalCase()
        {
            var data = Dao.AJJBXXHandler.Handler.TotalCase();
            return ResultOk<List<Model.CountCase>>(data);
        }
    }
}

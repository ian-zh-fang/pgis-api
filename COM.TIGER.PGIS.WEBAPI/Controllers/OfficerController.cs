using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class OfficerController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Officer>(v);
                var data = Dao.OfficerHandler.Handler.InsertEntity(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Officer>(v);
                var data = Dao.OfficerHandler.Handler.UpdateEntity(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.OfficerHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingOfficers(int index, int size)
        {
            var records = 0;
            var data = Dao.OfficerHandler.Handler.Page(index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Officer>> GetOfficers()
        {
            var data = Dao.OfficerHandler.Handler.GetEntities();
            return ResultOk<List<Model.Officer>>(data);
        }
    }
}

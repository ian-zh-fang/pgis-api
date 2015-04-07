using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class StreetNumController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.StreetNum>(v);
                return ResultOk<int>(Dao.StreetNumHandler.Handler.InsertEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.StreetNum>(v);
                return ResultOk<int>(Dao.StreetNumHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.StreetNumHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.StreetNum>> GetEntities()
        {
            var data = Dao.StreetNumHandler.Handler.GetEntities();
            return ResultOk<List<Model.StreetNum>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.StreetNum>> GetEntities(int streetid)
        {
            var data = Dao.StreetNumHandler.Handler.GetEntities(streetid);
            return ResultOk<List<Model.StreetNum>>(data);
        }
    }
}

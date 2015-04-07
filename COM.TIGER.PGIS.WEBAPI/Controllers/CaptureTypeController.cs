using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class CaptureTypeController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CaptureType>> GetCaptureTypes()
        {
            var data = Dao.CaptureTypeHandler.Handler.GetEntities();
            return ResultOk<List<Model.CaptureType>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CaptureType>> GetCaptureTypes(string ids)
        {
            var data = Dao.CaptureTypeHandler.Handler.GetEntities(ids);
            return ResultOk<List<Model.CaptureType>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.CaptureType>(v);
                var data = Dao.CaptureTypeHandler.Handler.InsertEntity(e);
                return ResultOk<int>(data);
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.CaptureType>(v);
                var data = Dao.CaptureTypeHandler.Handler.UpdateEntity(e);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.CaptureTypeHandler.Handler.DeleteEntities(ids.Split(','));
            return ResultOk<int>(data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    /// <summary>
    /// 违停抓拍
    /// </summary>
    public class CaptureController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Capture>> PagingCaptures(int index, int size)
        {
            var records = 0;
            var data = Dao.CaptureHandler.Handler.Pagging(index, size, out records);
            return ResultPagingEx<Model.Capture>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Capture>> GetCaptures()
        {
            var data = Dao.CaptureHandler.Handler.GetEntities();
            return ResultOk<List<Model.Capture>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Capture>(v);
                var data = Dao.CaptureHandler.Handler.InsertEntity(e);
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Capture>(v);
                var data = Dao.CaptureHandler.Handler.UpdateEntity(e);
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
            var data = Dao.CaptureHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }
    }
}

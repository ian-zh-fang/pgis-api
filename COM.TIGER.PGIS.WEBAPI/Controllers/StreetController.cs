using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class StreetController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Street>(v);
                return ResultOk<int>(Dao.StreetHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Street>(v);
                return ResultOk<int>(Dao.StreetHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.StreetHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Street>> GetEntities()
        {
            var data = Dao.StreetHandler.Handler.GetEntities();
            return ResultOk<List<Model.Street>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Street>> GetEntities(int adminid)
        {
            var data = Dao.StreetHandler.Handler.GetEntities(adminid.ToString());
            return ResultOk<List<Model.Street>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Street>> PagingStreets(int index, int size)
        {
            var records = 0;
            var data = Dao.StreetHandler.Handler.Page(index, size, out records);
            return ResultPagingEx<Model.Street>(data, records);
        }
    }
}

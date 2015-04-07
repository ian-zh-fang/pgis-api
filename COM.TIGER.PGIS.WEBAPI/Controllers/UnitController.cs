using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class UnitController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Unit>(v);
                return ResultOk<int>(Dao.UnitHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Unit>(v);
                return ResultOk<int>(Dao.UnitHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var rst = Dao.UnitHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(rst);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Unit>> GetEntities()
        {
            var data = Dao.UnitHandler.Handler.GetEntities();
            return ResultOk<List<Model.Unit>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Unit>> GetEntities(string ids)
        {
            var data = Dao.UnitHandler.Handler.GetEntities(ids);
            return ResultOk<List<Model.Unit>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Unit>> PagingUnits(int index, int size)
        {
            var records = 0;
            var data = Dao.UnitHandler.Handler.Page(index, size, out records);
            return ResultPagingEx<Model.Unit>(data, records);
        }
    }
}

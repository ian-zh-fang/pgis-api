using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class MarkController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Mark>> GetMarks()
        {
            return ResultOk<List<Model.Mark>>(Dao.MarkHandler.Handler.GetMarks());
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Mark>> GetMarks(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return ResultOk<List<Model.Mark>>(Dao.MarkHandler.Handler.GetMarks());

            return ResultOk<List<Model.Mark>>(Dao.MarkHandler.Handler.GetMarks(ids));
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingMarks(int index, int size)
        {
            var records = 0;
            var data = Dao.MarkHandler.Handler.Paging(index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingMarks(int index, int size, string name, int typeid)
        {
            var records = 0;
            var data = Dao.MarkHandler.Handler.Paging(index, size, name, typeid, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Mark>(v);
                return ResultOk<int>(Dao.MarkHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Mark>(v);
                return ResultOk<int>(Dao.MarkHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            return ResultOk<int>(Dao.MarkHandler.Handler.DeleteEntities(ids));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class TagController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Tag>> GetTags()
        {
            return ResultOk<List<Model.Tag>>(Dao.TagHandler.Handler.GetTags());
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Tag>> GetTags(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return ResultOk<List<Model.Tag>>(Dao.TagHandler.Handler.GetTags());

            return ResultOk<List<Model.Tag>>(Dao.TagHandler.Handler.GetTags(ids));
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTags(int index, int size)
        {
            var records = 0;
            var data = Dao.TagHandler.Handler.Paging(index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Tag>(v);
                return ResultOk<int>(Dao.TagHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Tag>(v);
                return ResultOk<int>(Dao.TagHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            return ResultOk<int>(Dao.TagHandler.Handler.DeleteEntities(ids));
        }
    }
}

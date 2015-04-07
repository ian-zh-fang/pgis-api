using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class OwnerPicController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OwnerPic>(v);
                return ResultOk<int>(Dao.OwnerPicHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OwnerPic>(v);
                return ResultOk<int>(Dao.OwnerPicHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var rst = Dao.OwnerPicHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(rst);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.OwnerPic>> PagingOwnerPics(int index, int size)
        {
            var records = 0;
            var data = Dao.OwnerPicHandler.Handler.Page(index, size, out records);
            return ResultPagingEx<Model.OwnerPic>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.OwnerPic>> PagingOwnerPics(int index, int size, string ids)
        {
            var records = 0;
            var data = Dao.OwnerPicHandler.Handler.Page(index, size, out records, ids);
            return ResultPagingEx<Model.OwnerPic>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.OwnerPic>> GetEntities()
        {
            var data = Dao.OwnerPicHandler.Handler.GetEntities();
            return ResultOk<List<Model.OwnerPic>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.OwnerPic>> GetEntities(string ids)
        {
            var data = Dao.OwnerPicHandler.Handler.GetEntities(ids);
            return ResultOk<List<Model.OwnerPic>>(data);
        }
    }
}

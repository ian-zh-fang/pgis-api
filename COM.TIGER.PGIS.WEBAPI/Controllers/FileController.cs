using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class FileController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.File>> GetFiles()
        {
            var data = Dao.FileHandler.Handler.GetFiles();
            return ResultOk<List<Model.File>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.File>> GetFiles(string ids)
        {
            var data = Dao.FileHandler.Handler.GetFiles(ids);
            return ResultOk<List<Model.File>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.File>> PagingFiles(int index, int size)
        {
            var records = 0;
            var data = Dao.FileHandler.Handler.Paging(index, size, out records);
            return ResultPagingEx<Model.File>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.File>(v);
                return ResultOk<int>(Dao.FileHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.File>(v);
                return ResultOk<int>(Dao.FileHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.FileHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }
    }
}

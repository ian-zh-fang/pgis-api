using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class BelongToController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.BelongTo>> GetBelongTos()
        {
            var data = Dao.BelongToHandler.Handler.GetEntities();
            return ResultOk<List<Model.BelongTo>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.BelongTo>> GetBelongTos(string ids)
        {
            if (ids == null) return GetBelongTos();

            var data = Dao.BelongToHandler.Handler.GetEntities(ids);
            return ResultOk<List<Model.BelongTo>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingBelongTos(int index, int size)
        {
            var records = 0;
            var data = Dao.BelongToHandler.Handler.Paging(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.BelongTo>(v);
                var data = Dao.BelongToHandler.Handler.InsertEntity(e);
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.BelongTo>(v);
                if (e.ID == 0) return ResultFaild<int>("更新条件不存在，必须指定需要更新记录的ID");

                var data = Dao.BelongToHandler.Handler.UpdateEntity(e);
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
            var data = Dao.BelongToHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }
    }
}

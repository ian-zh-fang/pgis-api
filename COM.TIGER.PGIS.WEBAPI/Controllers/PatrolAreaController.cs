using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class PatrolAreaController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.PatrolArea>> GetPatrolAreas()
        {
            var data = Dao.PatrolAreaHandler.Handler.GetPatrolArea();
            return ResultOk<List<Model.PatrolArea>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.PatrolArea>> PagingPatrolAreas(int index, int size)
        {
            var records = 0;
            var data = Dao.PatrolAreaHandler.Handler.Page(index, size, out records);
            return ResultPagingEx<Model.PatrolArea>(data, records);
        }
        
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntity(int id)
        {
            var data = Dao.PatrolAreaHandler.Handler.DeleteEntity(id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.PatrolAreaHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.PatrolArea>(v);
                var data = Dao.PatrolAreaHandler.Handler.InsertEntity(e);
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.PatrolArea>(v);
                return ResultOk<int>(Dao.PatrolAreaHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }
    }
}

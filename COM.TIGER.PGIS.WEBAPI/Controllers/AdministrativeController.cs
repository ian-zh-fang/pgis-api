using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class AdministrativeController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Administrative>(v);
                return ResultOk<int>(Dao.AdministrativeHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Administrative>(v);
                return ResultOk<int>(Dao.AdministrativeHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.AdministrativeHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Administrative>> GetAdministratives()
        {
            var data = Dao.AdministrativeHandler.Handler.GetEntities();
            return ResultOk<List<Model.Administrative>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Administrative>> GetTopAdmins()
        {
            var data = Dao.AdministrativeHandler.Handler.GetTopEntities();
            return ResultOk<List<Model.Administrative>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Administrative>> GetAdministratives(int id)
        {
            var data = Dao.AdministrativeHandler.Handler.GetEntities(id);
            return ResultOk<List<Model.Administrative>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Administrative>> PagingAdministratives(int index, int size)
        {
            var records = 0;
            var data = Dao.AdministrativeHandler.Handler.Page(index, size, out records);
            return ResultPagingEx<Model.Administrative>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Administrative>> PagingTop(int index, int size)
        {
            var records = 0;
            var data = Dao.AdministrativeHandler.Handler.PageTop(index, size, out records);
            return ResultPagingEx<Model.Administrative>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Administrative>> PagingSub(int id, int index, int size)
        {
            var records = 0;
            var data = Dao.AdministrativeHandler.Handler.PageSub(id, index, size, out records);
            return ResultPagingEx<Model.Administrative>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Administrative>> GetAdministrativeTree()
        {
            var data = Dao.AdministrativeHandler.Handler.GetEntitiesTree();
            return ResultOk<List<Model.Administrative>>(data);
        }
    }
}

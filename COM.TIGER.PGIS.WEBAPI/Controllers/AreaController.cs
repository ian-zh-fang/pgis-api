using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class AreaController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Area>> GetAreas()
        {
            var data = Dao.AreaHandler.Handler.GetAreas();
            return ResultOk<List<Model.Area>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Area>> GetAreasTree()
        {
            var data = Dao.AreaHandler.Handler.GetAreas(true);
            return ResultOk<List<Model.Area>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Area>> GetBelongToArea(int belongtoID)
        {
            var data = Dao.AreaHandler.Handler.GetAreas(belongtoID);
            return ResultOk<List<Model.Area>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Area>> GetTopAreas()
        {
            var data = Dao.AreaHandler.Handler.GetTopAreas();
            return ResultOk<List<Model.Area>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingAreas(int index, int size)
        {
            var records = 0;
            var data = Dao.AreaHandler.Handler.PagingArea(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingAreas(int index)
        {
            return PagingAreas(index, 10);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingAreas(int pid, int index, int size)
        {
            var records = 0;
            var data = Dao.AreaHandler.Handler.PagingAreas(pid, index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopAreas(int index, int size)
        {
            var records = 0;
            var data = Dao.AreaHandler.Handler.PagingTopArea(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopAreas(int index)
        {
            return PagingTopAreas(index, 10);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Area> GetEntity(int id, bool flag)
        {
            var data = Dao.AreaHandler.Handler.GetEntity(id, flag);
            return ResultOk<Model.Area>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Area> GetEntity(int id)
        {
            return GetEntity(id, true);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntitiesByID(int id, bool flag)
        {
            var data = Dao.AreaHandler.Handler.GetAreasByID(id, flag);
            return ResultOk<object>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntitiesByID(int id)
        {
            return GetEntitiesByID(id, true);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Area>> GetAreas(int id)
        {
            var data = Dao.AreaHandler.Handler.GetAreasByID(id);
            return ResultOk<List<Model.Area>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Area>> GetSubAreas(int id, bool flag)
        {
            var data = Dao.AreaHandler.Handler.GetAreas(id, flag);
            return ResultOk<List<Model.Area>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Area>> GetSubAreas(int id)
        {
            return GetSubAreas(id, false);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams(int pid, string name, decimal range, 
            string coordinates, string newcode, string oldcode, int belongtoid)
        {
            var e = new Model.Area() { BelongToID = belongtoid,
                Name = name, NewCode = newcode, OldCode = oldcode, PID = pid};
            return InsertNew(e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams()
        {
            try
            {
                var e = GetQueryParamsCollection<Model.Area>();
                return InsertNew(e);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Area>(v);
                return InsertNew(e);
            }
            catch(Exception e) 
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id)
        {
            var e = Dao.AreaHandler.Handler.GetEntity(id, false);
            e = GetQueryParamsCollection<Model.Area>(e);
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id, int pid, string name, decimal range,
            string coordinates, string newcode, string oldcode, int belongtoid)
        {
            var e = new Model.Area() { BelongToID = belongtoid, Name = name, NewCode = newcode, OldCode = oldcode, PID = pid };
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Area>(v);
                if (e.ID == 0) return ResultFaild<int>("更新条件不存在，必须指定需要更新记录的ID");

                return UpdateNew(e.ID, e);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntity(int id)
        {
            var data = Dao.AreaHandler.Handler.DeleteEntity(id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.AreaHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        private ApiResult<int> InsertNew(Model.Area e)
        {
            var data = Dao.AreaHandler.Handler.InsertNew(e);
            return ResultOk<int>(data);
        }
        
        private ApiResult<Model.Area> InsertEntity(Model.Area e)
        {
            var data = Dao.AreaHandler.Handler.InsertEntity(e);
            return ResultOk<Model.Area>(data);
        }
        
        private ApiResult<int> UpdateNew(int id, Model.Area e)
        {
            var data = Dao.AreaHandler.Handler.UpdateNew(id, e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Area> UpdateEntity(int id, Model.Area e)
        {
            var data = Dao.AreaHandler.Handler.UpdateEntity(id, e);
            return ResultOk<Model.Area>(data);
        }
    }
}

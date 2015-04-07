using COM.TIGER.PGIS.WEBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class ParamsController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Param>> GetParams()
        {
            var data = Dao.ParamHandler.Handler.GetParams();
            return ResultOk<List<Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingParams(int index, int size)
        {
            int records = 0;
            var data = Dao.ParamHandler.Handler.PagingParams(index, size, null, OrderType.Desc, out records, "id");
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingParams(int index)
        {
            int records = 0;
            var data = Dao.ParamHandler.Handler.PagingParams(index, 10, null, OrderType.Desc, out records, "Pgis_param.id");
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopParams(int index, int size)
        {
            var records = 0;
            var data = Dao.ParamHandler.Handler.PagingTopParams(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopParams(int index)
        {
            return PagingTopParams(index, 10);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Param> GetParamByID(int id)
        {
            var data = Dao.ParamHandler.Handler.GetEntity(id, true);
            return ResultOk<Param>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Param> GetParamByID(int id, bool flag)
        {
            var data = Dao.ParamHandler.Handler.GetEntity(id, flag);
            return ResultOk<Param>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Param> GetParamByCode(string code)
        {
            var data = Dao.ParamHandler.Handler.GetEntity(code, true);
            return ResultOk<Param>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Param> GetParamByCode(string code, bool flag)
        {
            var data = Dao.ParamHandler.Handler.GetEntity(code, flag);
            return ResultOk<Param>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntities(int id, bool flag)
        {
            var data = Dao.ParamHandler.Handler.GetParamsByID(id, flag);
            return ResultOk<object>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntities(int id)
        {
            return GetEntities(id, true);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetParams(int id)
        { 
            var data = Dao.ParamHandler.Handler.GetParamsByID(id);
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntities(string code, bool flag)
        {
            var data = Dao.ParamHandler.Handler.GetParamsByCode(code, flag);
            return ResultOk<object>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntities(string code)
        {
            return GetEntities(code, true);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetParams(string code)
        {
            var data = Dao.ParamHandler.Handler.GetParamsByCode(code);
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetSubParams(int id, bool flag)
        {
            var data = Dao.ParamHandler.Handler.GetParams(id, flag);
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetSubParams(int id)
        {
            return GetSubParams(id, false);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetSubParams(string code, bool flag)
        {
            var data = Dao.ParamHandler.Handler.GetParams(code, flag);
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetSubParams(string code)
        {
            return GetSubParams(code, false);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams(string code, int disabled, string name, int pid, int sort)
        {
            var e = new Model.Param() { Code = code, Disabled = disabled, Name = name, PID = pid, Sort = sort };
            return InsertNew(e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams()
        {
            try
            {
                var e = GetQueryParamsCollection<Model.Param>();
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                return InsertNew(e);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id)
        {
            var e = Dao.ParamHandler.Handler.GetEntity(id, false);
            e = GetQueryParamsCollection<Model.Param>(e);
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id, string code, int disabled, string name, int pid, int sort)
        {
            var e = new Model.Param() { Sort = sort, PID = pid, Name = name, Disabled = disabled, Code = code };
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
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
            var data = Dao.ParamHandler.Handler.DeleteEntity(id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.ParamHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }
        
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetGenders()
        {
            var data = Dao.ParamHandler.Handler.GetGenders();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetLiveTypes()
        {
            var data = Dao.ParamHandler.Handler.GetLiveTypes();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetEducations()
        {
            var data = Dao.ParamHandler.Handler.GetEducations();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetProvinces()
        {
            var data = Dao.ParamHandler.Handler.GetProvinces();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetCities()
        {
            var data = Dao.ParamHandler.Handler.GetCities();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetPoliticalStatus()
        {
            var data = Dao.ParamHandler.Handler.GetPoliticalStatus();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetBloodTypes()
        {
            var data = Dao.ParamHandler.Handler.GetBloodTypes();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetSoldierStatus()
        {
            var data = Dao.ParamHandler.Handler.GetSoldierStatus();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetMarriageStatus()
        {
            var data = Dao.ParamHandler.Handler.GetMarriageStatus();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetPsychosisTypes()
        {
            var data = Dao.ParamHandler.Handler.GetPsychosisTypes();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetHRelation()
        {
            var data = Dao.ParamHandler.Handler.GetHRelation();
            return ResultOk<List<Model.Param>>(data);
        }

        private ApiResult<int> InsertNew(Model.Param e)
        {
            var data = Dao.ParamHandler.Handler.InsertNew(e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Param> InsertEntity(Model.Param e)
        {
            var data = Dao.ParamHandler.Handler.InsertEntity(e);
            return ResultOk<Model.Param>(data);
        }

        private ApiResult<int> UpdateNew(int id, Model.Param e)
        {
            var data = Dao.ParamHandler.Handler.UpdateNew(id, e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Param> UpdateEntity(int id, Model.Param e)
        {
            var data = Dao.ParamHandler.Handler.UpdateEntity(id, e);
            return ResultOk<Model.Param>(data);
        }
    }
}

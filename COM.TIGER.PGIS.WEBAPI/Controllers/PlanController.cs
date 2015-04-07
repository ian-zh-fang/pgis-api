using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class PlanController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Plan>> GetPlans()
        {
            var data = Dao.PlanHandler.Handler.GetPlans();
            return ResultOk<List<Model.Plan>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Plan>> GetPlans(string ids)
        {
            var data = Dao.PlanHandler.Handler.GetPlans(ids);
            return ResultOk<List<Model.Plan>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingPlans(int index, int size)
        {
            var records = 0;
            var data = Dao.PlanHandler.Handler.Paging(index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Tag>> GetPlanTags(string ids)
        {
            var data = Dao.PlanHandler.Handler.GetPlanTags(ids);
            return ResultOk<List<Model.Tag>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.File>> GetPlanFiles(string ids)
        {
            var data = Dao.PlanHandler.Handler.GetPlanFiles(ids);
            return ResultOk<List<Model.File>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertPlanTagForJson(string v, int planid)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Tag>(v);
                var data = Dao.PlanHandler.Handler.InsertEntity(e, planid);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertPlanFileForJson(string v, int planid)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.File>(v);
                var data = Dao.PlanHandler.Handler.InsertEntity(e, planid);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertPlanFilesForJson(string v, int planid)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.File[]>(v);
                var data = Dao.PlanHandler.Handler.InsertEntity(e, planid);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertPlanFilesForJson(string v, string p)
        {
            try
            {
                var files = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.File[]>(v);
                var plan = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Plan>(p);
                var data = Dao.PlanHandler.Handler.InsertEntity(plan, files);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeletePlanTags(int planid, string ids)
        {
            var data = Dao.PlanHandler.Handler.DeletePlanTags(planid, ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeletePlanFiles(int planid, string ids)
        {
            var data = Dao.PlanHandler.Handler.DeletePlanFiles(planid, ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Plan>(v);
                return ResultOk<int>(Dao.PlanHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Plan>(v);
                return ResultOk<int>(Dao.PlanHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.PlanHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteFiles(string v)
        {
            var files = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.File[]>(v);
            var data = Dao.PlanHandler.Handler.DeleteEntities(files);
            return ResultOk<int>(data);
        }
    }
}

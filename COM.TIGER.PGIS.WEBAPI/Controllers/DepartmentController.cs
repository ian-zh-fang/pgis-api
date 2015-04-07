using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class DepartmentController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Department>> GetDepartments()
        {
            var data = Dao.DepartmentHandler.Handler.GetDepartments();
            return ResultOk<List<Model.Department>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Department>> GetDepartmentsTree()
        {
            var data = Dao.DepartmentHandler.Handler.GetDepartmentsTree();
            return ResultOk<List<Model.Department>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Department>> GetTopDepartments()
        {
            var data = Dao.DepartmentHandler.Handler.GetTopDepartments();
            return ResultOk<List<Model.Department>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingDepartments(int index, int size)
        {
            var records = 0;
            var data = Dao.DepartmentHandler.Handler.PagingDepartment(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingDepartments(int index)
        {
            return PagingDepartments(index, 10);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopDepartments(int index, int size)
        {
            var records = 0;
            var data = Dao.DepartmentHandler.Handler.PagingTopDepartment(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopDepartments(int index)
        {
            return PagingTopDepartments(index, 10);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Department> GetEntity(int id, bool flag)
        {
            var data = Dao.DepartmentHandler.Handler.GetEntity(id, flag);
            return ResultOk<Model.Department>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Department> GetEntity(int id)
        {
            return GetEntity(id, true);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntitiesByID(int id, bool flag)
        { 
            var data = Dao.DepartmentHandler.Handler.GetDepartmentsByID(id, flag);
            return ResultOk<object>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEntitiesByID(int id)
        {
            return GetEntitiesByID(id, true);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Department>> GetDepartments(int id)
        {
            var data = Dao.DepartmentHandler.Handler.GetDepartmentsByID(id);
            return ResultOk<List<Model.Department>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Department>> GetSubDepartments(int id, bool flag)
        {
            var data = Dao.DepartmentHandler.Handler.GetDepartments(id, flag);
            return ResultOk<List<Model.Department>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Department>> GetSubDepartments(int id)
        {
            return GetSubDepartments(id, false);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams(string code, string name, int pid, string remarks)
        {
            var e = new Model.Department()
            {
                Code = code,
                Name = name,
                PID = pid,
                Remarks = remarks
            };
            return InsertNew(e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams()
        {
            try
            {
                var e = GetQueryParamsCollection<Model.Department>();
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Department>(v);
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
            var e = Dao.DepartmentHandler.Handler.GetEntity(id, false);
            e = GetQueryParamsCollection<Model.Department>(e);
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id, string code, string name, int pid, string remarks)
        {
            var e = new Model.Department()
            {
                Code = code,
                Name = name,
                PID = pid,
                Remarks = remarks
            }; 
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Department>(v);
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
            var data = Dao.DepartmentHandler.Handler.DeleteEntity(id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.DepartmentHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        private ApiResult<int> InsertNew(Model.Department e)
        {
            var data = Dao.DepartmentHandler.Handler.InsertNew(e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Department> InsertEntity(Model.Department e)
        {
            var data = Dao.DepartmentHandler.Handler.InsertEntity(e);
            return ResultOk<Model.Department>(data);
        }

        private ApiResult<int> UpdateNew(int id, Model.Department e)
        {
            var data = Dao.DepartmentHandler.Handler.UpdateNew(id, e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Department> UpdateEntity(int id, Model.Department e)
        {
            var data = Dao.DepartmentHandler.Handler.UpdateEntity(id, e);
            return ResultOk<Model.Department>(data);
        }
    }
}

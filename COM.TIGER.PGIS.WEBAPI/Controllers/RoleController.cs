using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class RoleController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Role>> GetRoles()
        {
            var data = Dao.RoleHandler.Handler.GetRoles();
            return ResultOk<List<Model.Role>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingRoles(int index, int size)
        {
            var records = 0;
            var data = Dao.RoleHandler.Handler.PagingRoles(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingRoles(int index)
        {
            return PagingRoles(index, 10);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Role>> GetUserRoles(int userID)
        {
            var data = Dao.RoleHandler.Handler.GetUserRole(userID);
            return ResultOk<List<Model.Role>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Role> GetEntity(int id)
        {
            var data = Dao.RoleHandler.Handler.GetEntity(id);
            return ResultOk<Model.Role>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams(string name, string remarks)
        {
            var e = new Model.Role()
            {
                Name = name,
                Remarks = remarks
            };
            return InsertNew(e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams()
        {
            try
            {
                var e = GetQueryParamsCollection<Model.Role>();
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Role>(v);
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
            var e = Dao.RoleHandler.Handler.GetEntity(id);
            e = GetQueryParamsCollection<Model.Role>(e);
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id, string name, string remarks)
        {
            var e = new Model.Role()
            {
                Name = name,
                Remarks = remarks
            };
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Role>(v);
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
            var data = Dao.RoleHandler.Handler.DeleteEntity(id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.RoleHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.RoleMenu>> GetRoleMenus(int id)
        {
            var data = Dao.RoleHandler.Handler.GetRoleMenus(id);
            return ResultOk<List<Model.RoleMenu>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> SaveRoleMenus(int id, string ids)
        {
            if (ids == null) return ResultOk<int>(Dao.RoleHandler.Handler.SaveRoleMenus(id));
            var items = ids.Split(new char[] { ',', ':', ';', '|', '#', '$', '(', ')', '[', ']', '{', '}', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            var data = Dao.RoleHandler.Handler.SaveRoleMenus(id, items);
            return ResultOk<int>(data);
        }

        private ApiResult<int> InsertNew(Model.Role e)
        {
            var data = Dao.RoleHandler.Handler.InsertNew(e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Role> InsertEntity(Model.Role e)
        {
            var data = Dao.RoleHandler.Handler.InsertEntity(e);
            return ResultOk<Model.Role>(data);
        }

        private ApiResult<int> UpdateNew(int id, Model.Role e)
        {
            var data = Dao.RoleHandler.Handler.UpdateNew(id, e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Role> UpdateEntity(int id, Model.Role e)
        {
            var data = Dao.RoleHandler.Handler.UpdateEntity(id, e);
            return ResultOk<Model.Role>(data);
        }
    }
}

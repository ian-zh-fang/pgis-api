using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class UserController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.User>> GetUsers()
        {
            var data = Dao.UserHandler.Handler.GetUsers();
            return ResultOk<List<Model.User>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingUsers(int index, int size)
        {
            var records = 0;
            var data = Dao.UserHandler.Handler.PagingUsers(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingUsers(int index)
        {
            return PagingUsers(index, 10);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.User>> GetDepartmentUsers(int departmentID)
        {
            var data = Dao.UserHandler.Handler.GetDepartmentUsers(departmentID);
            return ResultOk<List<Model.User>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.User>> GetRoleUsers(int roleID)
        {
            var data = Dao.UserHandler.Handler.GetRoleUser(roleID);
            return ResultOk<List<Model.User>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.User> GetEntity(int id)
        {
            var data = Dao.UserHandler.Handler.GetEntity(id);
            return ResultOk<Model.User>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.User> GetEntity(string username)
        {
            var data = Dao.UserHandler.Handler.GetEntity(username);
            return ResultOk<Model.User>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams(string code, int departmentid, int disabled, int gender, int lvl,
                string name, string password, string username)
        {
            var e = new Model.User()
            {
                Code = code,
                DepartmentID = departmentid,
                Disabled = disabled,
                Gender = gender,
                Lvl = lvl,
                Name = name,
                Password = password,
                UserName = username
            };
            return InsertNew(e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams()
        {
            try
            {
                var e = GetQueryParamsCollection<Model.User>();
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.User>(v);
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
            var e = Dao.UserHandler.Handler.GetEntity(id);
            e = GetQueryParamsCollection<Model.User>(e);
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id, string code, int departmentid, int disabled, int gender, int lvl,
                string name, string password, string username)
        {
            var e = new Model.User()
            {
                Code = code,
                DepartmentID = departmentid,
                Disabled = disabled,
                Gender = gender,
                Lvl = lvl,
                Name = name,
                Password = password,
                UserName = username
            };
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.User>(v);
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
            var data = Dao.UserHandler.Handler.DeleteEntity(id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.UserHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.UserRole>> GetUserRoles(int id)
        {
            var data = Dao.UserHandler.Handler.GetUserRoles(id);
            return ResultOk<List<Model.UserRole>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> SaveUserRoles(int id, string ids)
        {
            if (ids == null) return ResultOk<int>(Dao.UserHandler.Handler.SaveUserRoles(id));
            var items = ids.Split(new char[] { ',', ':', ';', '|', '#', '$', '(', ')', '[', ']', '{', '}', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            var data = Dao.UserHandler.Handler.SaveUserRoles(id, items);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.User> CheckUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) return ResultOk<Model.User>(null);
            if (string.IsNullOrWhiteSpace(password)) return ResultOk<Model.User>(null);

            var user = Dao.UserHandler.Handler.GetUsersOnUserNameOrOfficerNum(username, password);
            return ResultOk<Model.User>(user);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> ChangePassword(string password, int id)
        {
            var data = Dao.UserHandler.Handler.ChangePassword(password, id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> ChangeInfo(int id, string name, string identityid, string tel, int gender)
        {
            var data = Dao.UserHandler.Handler.ChangeInfo(id, name, identityid, tel, gender);
            return ResultOk<int>(data);
        }

        private ApiResult<int> InsertNew(Model.User e)
        {
            var data = Dao.UserHandler.Handler.InsertNew(e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.User> InsertEntity(Model.User e)
        {
            var data = Dao.UserHandler.Handler.InsertEntity(e);
            return ResultOk<Model.User>(data);
        }

        private ApiResult<int> UpdateNew(int id, Model.User e)
        {
            var data = Dao.UserHandler.Handler.UpdateNew(id, e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.User> UpdateEntity(int id, Model.User e)
        {
            var data = Dao.UserHandler.Handler.UpdateEntity(id, e);
            return ResultOk<Model.User>(data);
        }
    }
}

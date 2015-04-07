using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class MenuController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Menu>> GetMenus()
        {
            var data = Dao.MenuHandler.Handler.GetMenus();
            return ResultOk<List<Model.Menu>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Menu>> GetMenusTree()
        {
            var data = Dao.MenuHandler.Handler.GetMenusTree();
            return ResultOk<List<Model.Menu>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Menu>> GetTopMenus()
        {
            var data = Dao.MenuHandler.Handler.GetTopMenus();
            return ResultOk<List<Model.Menu>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Menu>> GetMenusOnRoles(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                return ResultOk<List<Model.Menu>>(new List<Model.Menu>());

            var data = Dao.MenuHandler.Handler.GetMenusOnRoles(ids).DistinctBy(t => new { t.ID }).ToList();
            return ResultOk<List<Model.Menu>>(data);
        }


        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Menu>> GetMenusOnUser(int id)
        {
            var data = Dao.MenuHandler.Handler.GetMenusOnUser(id).DistinctBy(t => new { t.ID }).ToList();
            return ResultOk<List<Model.Menu>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopMenus(int index, int size)
        {
            var records = 0;
            var data = Dao.MenuHandler.Handler.PagingTopMenus(index, size, out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingTopMenus(int index, int size, string sortfield)
        {
            var records = 0;
            var data = Dao.MenuHandler.Handler.PagingTopMenus(index, size, sortfield,out records);
            return ResultOk<object>(new { TotalRecords = records, Data = data });
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Menu> GetEntity(int id, bool flag)
        {
            var data = Dao.MenuHandler.Handler.GetEntity(id, flag);
            return ResultOk<Model.Menu>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<Model.Menu> GetEntity(int id)
        {
            return GetEntity(id, true);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Menu>> GetSubMenus(int id, bool flag)
        {
            var data = Dao.MenuHandler.Handler.GetMenus(id, flag);
            return ResultOk<List<Model.Menu>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Menu>> GetSubMenus(int id)
        {
            return GetSubMenus(id, false);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams(int checkbox, string code, string description, int disabled, string handler,
                string iconcls, int pid, int sort, string text)
        {
            var e = new Model.Menu()
            {
                Checked = checkbox,
                Code = code,
                Description = description,
                Disabled = disabled,
                Handler = handler,
                Iconcls = iconcls,
                PID = pid,
                Sort = sort,
                Text = text
            };
            return InsertNew(e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForParams()
        {
            try
            {
                var e = GetQueryParamsCollection<Model.Menu>();
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Menu>(v);
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
            var e = Dao.MenuHandler.Handler.GetEntity(id, false);
            e = GetQueryParamsCollection<Model.Menu>(e);
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewParams(int id, int checkbox, string code, string description, int disabled, string handler,
                string iconcls, int pid, int sort, string text)
        {
            var e = new Model.Menu()
            {
                Checked = checkbox,
                Code = code,
                Description = description,
                Disabled = disabled,
                Handler = handler,
                Iconcls = iconcls,
                PID = pid,
                Sort = sort,
                Text = text
            };
            return UpdateNew(id, e);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Menu>(v);
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
            var data = Dao.MenuHandler.Handler.DeleteEntity(id);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.MenuHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        private ApiResult<int> InsertNew(Model.Menu e)
        {
            var data = Dao.MenuHandler.Handler.InsertNew(e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Menu> InsertEntity(Model.Menu e)
        {
            var data = Dao.MenuHandler.Handler.InsertEntity(e);
            return ResultOk<Model.Menu>(data);
        }

        private ApiResult<int> UpdateNew(int id, Model.Menu e)
        {
            var data = Dao.MenuHandler.Handler.UpdateNew(id, e);
            return ResultOk<int>(data);
        }

        private ApiResult<Model.Menu> UpdateEntity(int id, Model.Menu e)
        {
            var data = Dao.MenuHandler.Handler.UpdateEntity(id, e);
            return ResultOk<Model.Menu>(data);
        }
    }
}

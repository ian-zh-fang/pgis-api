using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class CompanyController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Company>(v);
                var data = Dao.CompanyHandler.Handler.InsertEntity(e) ;
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateNewJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Company>(v);
                var data = Dao.CompanyHandler.Handler.UpdateEntity(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.CompanyHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Company>> PagingCompanys(int index, int size)
        {
            int records = 0;
            var data = Dao.CompanyHandler.Handler.PagingCompanys(index, size, out records);
            return ResultPagingEx<Model.Company>(data, records);
        }
        
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Company>> QueryCompany(string name, string addr, int index, int size)
        {
            var records = 0;
            var data = Dao.CompanyHandler.Handler.QueryCompany(name, addr, index, size, out records);
            return ResultPagingEx<Model.Company>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.Company>> PageCompanies(string query, int index, int size)
        {
            var records = 0;
            var data = Dao.CompanyHandler.Handler.PageCompanies(query, index, size, out records);
            return ResultPagingEx<Model.Company>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetCompanyTrades()
        {
            var data = Dao.CompanyHandler.Handler.GetCompanyTrades();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetCompanyGenres()
        {
            var data = Dao.CompanyHandler.Handler.GetCompanyGenres();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetCompanyTypes()
        {
            var data = Dao.CompanyHandler.Handler.GetCompanyTypes();
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddKind(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.CompanyHandler.Handler.AddKind(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }            
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddGenre(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.CompanyHandler.Handler.AddGenre(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddTrade(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.CompanyHandler.Handler.AddTrade(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateParam(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.CompanyHandler.Handler.UpdateParam(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) { return ResultException<int>(e.Message); }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteParams(string ids)
        {
            var data = Dao.CompanyHandler.Handler.DeleteParams(ids);
            return ResultOk<int>(data);
        }
    }
}

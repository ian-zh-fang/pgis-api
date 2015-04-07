using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class CompanyMarkController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<PagingModel<Model.CompanyMark>> PagingCompanyMarks(int index, int size, int? type)
        {
            var records = 0;
            if (type == null) {
                return ResultPagingEx<Model.CompanyMark>(Dao.CompanyMarkHandler.Handler.Paging(index, size, out records), records);
            }
            var data = Dao.CompanyMarkHandler.Handler.Paging(index, size,(int)type, out records);
            return ResultPagingEx<Model.CompanyMark>(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.CompanyMark>(v);
                return ResultOk<int>(Dao.CompanyMarkHandler.Handler.InsertEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.CompanyMarkHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }
    }
}

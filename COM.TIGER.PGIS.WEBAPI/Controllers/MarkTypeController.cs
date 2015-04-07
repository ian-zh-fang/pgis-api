using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    /// <summary>
    /// 标注类型接口处理程序
    /// </summary>
    public class MarkTypeController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.MarkType>> GetMarkTypes()
        {
            var list = Dao.MarkTypeHandler.Handler.GetEntities();
            return ResultOk<List<Model.MarkType>>(list);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.MarkType>> GetMarkTypes(string ids)
        {
            var list = Dao.MarkTypeHandler.Handler.GetEntities(ids);
            return ResultOk<List<Model.MarkType>>(list);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.MarkType>> GetMarkTypeMarks()
        {
            var data = Dao.MarkTypeHandler.Handler.GetEntitiesForTree();
            return ResultOk<List<Model.MarkType>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.MarkType>(v);
                var rst = Dao.MarkTypeHandler.Handler.InsertEntity(e);
                return ResultOk<int>(rst);
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.MarkType>(v);
                var rst = Dao.MarkTypeHandler.Handler.UpdateEntity(e);
                return ResultOk<int>(rst);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var rst = Dao.MarkTypeHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(rst);
        }
    }
}

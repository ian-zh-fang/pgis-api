using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class RangeController : BaseApiController
    {
        /// <summary>
        /// 获取指定AreaID的区域范围信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.AreaRange>> GetRanges(int id)
        {
            var data = Dao.AreaRangeHandler.Handler.GetAllEntities(id);
            return ResultOk<List<Model.AreaRange>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.AreaRange>(v);
                var data = Dao.AreaRangeHandler.Handler.Add(e);
                return ResultOk<int>(data);
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.AreaRange>(v);
                if (e.ID == 0) return ResultFaild<int>("更新条件不存在，必须指定需要更新记录的ID");

                var data = Dao.AreaRangeHandler.Handler.Update(e);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var data = Dao.AreaRangeHandler.Handler.Delete(ids);
            return ResultOk<int>(data);
        }
    }
}

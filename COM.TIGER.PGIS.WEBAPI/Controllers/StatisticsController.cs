using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class StatisticsController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CountCase>> CountCase()
        {
            var data = Dao.StatisticsHandler.Handler.CountCase();
            return ResultOk<List<Model.CountCase>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CountCompany>> CountCompany()
        {
            var data = Dao.StatisticsHandler.Handler.CountCompany();
            return ResultOk<List<Model.CountCompany>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CountHotel>> CountHotel()
        {
            var data = Dao.StatisticsHandler.Handler.CountHotel();
            return ResultOk<List<Model.CountHotel>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CountMonitor>> CountMonitor()
        {
            var data = Dao.StatisticsHandler.Handler.CountMonitor();
            return ResultOk<List<Model.CountMonitor>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.CountPopulation>> CountPopulation()
        {
            var data = Dao.StatisticsHandler.Handler.CountPopulation();
            return ResultOk<List<Model.CountPopulation>>(data);
        }
    }
}

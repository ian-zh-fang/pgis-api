using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    /// <summary>
    /// 监控设备信息API
    /// </summary>
    public class MonitorDeviceController : BaseApiController
    {
        const string PARENTTYPECODE = "monitortype";
        const string PARENTDOTYPECODE = "monitordotype";
        Model.Param _type = null;
        Model.Param _dotype = null;

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {            
            base.Initialize(controllerContext);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddType(string v)
        {
            _type = Dao.ParamHandler.Handler.GetEntityByCode(PARENTTYPECODE);
            if (_type == null)
                return ResultFaild<int>("系统参数中没有找到监控设备类型参数");
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                e.PID = _type.ID;
                var data = Dao.ParamHandler.Handler.InsertNew(e);
                return ResultOk<int>(data);
            }
            catch (Exception e) 
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateType(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.ParamHandler.Handler.UpdateNew(e.ID, e);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteTypes(string ids)
        {
            var data = Dao.ParamHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddDoType(string v)
        {
            _dotype = Dao.ParamHandler.Handler.GetEntityByCode(PARENTDOTYPECODE);
            if (_dotype == null)
                return ResultFaild<int>("系统参数中没有找到监控设备用途参数");
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                e.PID = _dotype.ID;
                var data = Dao.ParamHandler.Handler.InsertNew(e);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateDoType(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Param>(v);
                var data = Dao.ParamHandler.Handler.UpdateNew(e.ID, e);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteDoTypes(string ids)
        {
            var data = Dao.ParamHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetTypes()
        {
            var data = Dao.ParamHandler.Handler.GetParams(PARENTTYPECODE, false);
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Param>> GetDoTypes()
        {
            var data = Dao.ParamHandler.Handler.GetParams(PARENTDOTYPECODE, false);
            return ResultOk<List<Model.Param>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddDevice(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.MonitorDevice>(v);
                var data = Dao.MonitorDeviceHandler.Handler.InsertEntity(e);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateDevice(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.MonitorDevice>(v);
                var data = Dao.MonitorDeviceHandler.Handler.UpdateEntity(e);
                return ResultOk<int>(data);
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteDevices(string ids)
        {
            var data = Dao.MonitorDeviceHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> Page(int index, int size)
        {
            var records = 0;
            var data = Dao.MonitorDeviceHandler.Handler.Page(index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PageQuery(string name, string num, int dotypeid, string address, int index, int size)
        {
            var records = 0;
            var data = Dao.MonitorDeviceHandler.Handler.PageQuery(name, num, dotypeid, address, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Address>> Match(string pattern)
        {
            var data = Dao.MonitorDeviceHandler.Handler.Match(pattern);
            return ResultOk<List<Model.Address>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.MonitorDevice>> Query(string coords)
        {
            if (string.IsNullOrWhiteSpace(coords))
                return ResultOk<List<Model.MonitorDevice>>(new List<Model.MonitorDevice>());

            var coordinates = (from t in coords.Split(',') select double.Parse(t)).ToArray();
            double x1, y1, x2, y2;
            GetXY(coordinates, out x1, out y1, out x2, out y2);
            //矩形选择
            if (coordinates.Length == 4)
            {
                return ResultOk<List<Model.MonitorDevice>>(Dao.MonitorDeviceHandler.Handler.Query(x1, y1, x2, y2));
            }

            //多边形选择
            if (coordinates.Length > 4)
            { 
                //首先计算当前最大矩形框内的所有设备信息
                //其次计算当前多边形内的所有设备信息
                return ResultOk<List<Model.MonitorDevice>>(Dao.MonitorDeviceHandler.Handler.Query(coordinates, x1, y1, x2, y2));
            }

            return ResultOk<List<Model.MonitorDevice>>(new List<Model.MonitorDevice>());
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.MonitorDevice>> GetMonitorDevices()
        {
            var data = Dao.MonitorDeviceHandler.Handler.GetDevices();
            return ResultOk<List<Model.MonitorDevice>>(data);
        }
    }
}

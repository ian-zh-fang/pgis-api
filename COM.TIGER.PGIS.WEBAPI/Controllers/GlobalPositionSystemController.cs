using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using COM.TIGER.PGIS.WEBAPI.Dao;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class GlobalPositionSystemController : BaseApiController
    {
        [HttpPost]
        public ApiResult<int> AddDeviceBinding(string v)
        {
            try
            {
                Model.GpsDevice t = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.GpsDevice>(v);
                return ResultOk<int>(GPSHandler.Handler.InsertEntity(t));
            }
            catch(Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpPost]
        public ApiResult<int> ModifyDeviceBinding(string v)
        {
            try
            {
                Model.GpsDevice t = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.GpsDevice>(v);
                return ResultOk<int>(GPSHandler.Handler.UpdateEntity(t));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpPost]
        public ApiResult<int> RemoveDeviceBinding(string ids)
        {
            return ResultOk<int>(GPSHandler.Handler.DeleteEntities(ids));
        }

        [HttpPost]
        public ApiResult<int> AddDeviceTrack([FromBody] Model.GpsDeviceTrack t)
        {
            return ResultOk<int>(GPSHandler.Handler.InsertDeviceTrack(t));
        }

        [HttpGet]
        public ApiResult<object> PageDevices(int index, int size)
        {
            int records = 0;
            List<Model.GpsDevice> data = GPSHandler.Handler.PageDevices(index, size, out records);

            return ResultPaging(data, records);
        }

        [HttpGet]
        public ApiResult<object> PageDevicesAtNumber(int index, int size, string number)
        {
            int records = 0;
            List<Model.GpsDevice> data = GPSHandler.Handler.PageDevicesAtNumber(index, size, number, out records);
            return ResultPaging(data, records);
        }

        [HttpGet]
        public ApiResult<object> PageDevicesAtOfficer(int index, int size, string officerid)
        {
            int records = 0;
            List<Model.GpsDevice> data = GPSHandler.Handler.PageDevicesAtOfficer(index, size, officerid, out records);
            return ResultPaging(data, records);
        }

        [HttpGet]
        public ApiResult<object> PageDevicesAtCar(int index, int size, string carid)
        {
            int records = 0;
            List<Model.GpsDevice> data = GPSHandler.Handler.PageDevicesAtCar(index, size, carid, out records);
            return ResultPaging(data, records);
        }

        [HttpGet]
        public ApiResult<List<Model.GpsDeviceTrack>> GetDevicesCurrentPosition()
        {
            List<Model.GpsDeviceTrack> data = GPSHandler.Handler.GetDevicesCurrentPosition();
            return ResultOk<List<Model.GpsDeviceTrack>>(data);
        }

        [HttpGet]
        public ApiResult<List<Model.GpsDeviceTrack>> GetDeviceHistoryPoints(string deviceId, DateTime start, DateTime end)
        {
            List<Model.GpsDeviceTrack> data = GPSHandler.Handler.GetDeviceHistoryPoints(deviceId, start, end);
            return ResultOk<List<Model.GpsDeviceTrack>>(data);
        }

        [HttpGet]
        public ApiResult<List<Model.GpsDeviceTrack>> GetDevicesCurrentPostionAtPanel(string coords)
        {
            List<Model.GpsDeviceTrack> list = new List<Model.GpsDeviceTrack>();

            if (string.IsNullOrWhiteSpace(coords))
                return ResultOk<List<Model.GpsDeviceTrack>>(list);

            double[] coordinates = (from t in coords.Split(',') select double.Parse(t)).ToArray();
            double x1, y1, x2, y2;
            GetXY(coordinates, out x1, out y1, out x2, out y2);

            //矩形选择
            if (coordinates.Length == 4)
            {
                list = GPSHandler.Handler.GetDevicesCurrentPostionAtPanel(x1, y1, x2, y2);
            }

            //多边形选择
            if (coordinates.Length > 4)
            {
                //首先计算当前最大矩形框内的所有设备信息
                //其次计算当前多边形内的所有设备信息
                list = GPSHandler.Handler.GetDevicesCurrentPostionAtPanel(coordinates, x1, y1, x2, y2);
            }

            return ResultOk<List<Model.GpsDeviceTrack>>(list);
        }
    }
}

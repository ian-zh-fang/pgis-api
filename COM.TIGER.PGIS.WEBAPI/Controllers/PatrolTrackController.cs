using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class PatrolTrackController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertNewForJson(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.PatrolTrack>(v);
                return ResultOk<int>(Dao.PatrolTrackHandler.Handler.InsertEntity(e));
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
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.PatrolTrack>(v);
                return ResultOk<int>(Dao.PatrolTrackHandler.Handler.UpdateEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> DeleteEntities(string ids)
        {
            var rst = Dao.PatrolTrackHandler.Handler.DeleteEntities(ids);
            return ResultOk<int>(rst);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.PatrolTrack>> GetPatrolTracks()
        {
            var data = Dao.PatrolTrackHandler.Handler.GetEntities();
            return ResultOk<List<Model.PatrolTrack>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingPatrolTracks(int index, int size)
        {
            var records = 0;
            var data = Dao.PatrolTrackHandler.Handler.Page(index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> AddTrackDevices(int trackid, string deviceids)
        {
            var data = Dao.PatrolTrackHandler.Handler.AddTrackDevices(trackid, deviceids);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.MonitorDeviceEx>> GetDevicesOnTrack(int trackid)
        {
            var data = Dao.PatrolTrackHandler.Handler.GetDevicesOnTrack(trackid);
            return ResultOk<List<Model.MonitorDeviceEx>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> InsertRecord(string v)
        {
            try
            {
                var e = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.PatrolRecord>(v);
                return ResultOk<int>(Dao.PatrolTrackHandler.Handler.InsertEntity(e));
            }
            catch (Exception e)
            {
                return ResultException<int>(e.Message);
            }
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PagingRecords(string devicename, string officername, DateTime? timestart, DateTime? timeend, int index, int size)
        {
            var records = 0;
            var data = Dao.PatrolTrackHandler.Handler.Page(devicename, officername, timestart, timeend, index, size, out records);
            return ResultPaging(data, records);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class HotelController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> PageHotels(int index, int size)
        {
            var records = 0;
            var data = Dao.HotelHandler.Handler.PageHotels(index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetOnHotel(int id, int index, int size)
        {
            var records = 0;
            var data = Dao.HotelHandler.Handler.GetOnHotel(id, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.HotelStay>> GetOnRoom(int id, string roomnum, string ptime)
        {
            var data = Dao.HotelHandler.Handler.GetOnRoom(id, roomnum, ptime);
            return ResultOk<List<Model.HotelStay>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.HotelStay>> GetOnTogether(int id, string ptime)
        {
            var data = Dao.HotelHandler.Handler.GetOnTogether(id, ptime);
            return ResultOk<List<Model.HotelStay>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetOnMove(string code, int index, int size)
        {
            var records = 0;
            var data = Dao.HotelHandler.Handler.GetOnMove(code, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<int> UpdateAddress(int id, string addr)
        {
            var data = Dao.HotelHandler.Handler.UpdateAddress(id, addr);
            return ResultOk<int>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Hotel>> MatchHotel(string name)
        {
            var data = Dao.HotelHandler.Handler.MatchHotel(name);
            return ResultOk<List<Model.Hotel>>(data);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> QueryHotel(string name, string addr, int index, int size)
        {
            var records = 0;
            var data = Dao.HotelHandler.Handler.QueryHotel(name, addr, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> QueryHotelStay(string name, string code, string hname, string roomnum, string ptime, string gtime, int index, int size)
        {
            var records = 0;
            var data = Dao.HotelHandler.Handler.QueryHotelStay(name, code, hname, roomnum, ptime, gtime, index, size, out records);
            return ResultPaging(data, records);
        }
    }
}

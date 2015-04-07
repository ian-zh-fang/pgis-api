using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class EmployeeController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> QueryEmployees(string name, string code, string addr, int index, int size)
        {
            var records = 0;
            var data = Dao.EmployeeHandler.Handler.QueryEmployees(name, code, addr, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEmployeesOnCompany(int id, int index, int size)
        {
            var records = 0;
            var data = Dao.EmployeeHandler.Handler.GetEmployeesOnCompany(id, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetQuitEmployeesOnCompany(int id, int index, int size)
        {
            var records = 0;
            var data = Dao.EmployeeHandler.Handler.GetQuitEmployeesOnCompany(id, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> QueryEmployeesOnCompany(string name, string code, string addr, int index, int size)
        {
            var records = 0;
            var data = Dao.EmployeeHandler.Handler.QueryEmployeesOnCompany(name, code, addr, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> QueryEmployeesOnHotel(string name, string code, string addr, int index, int size)
        {
            var records = 0;
            var data = Dao.EmployeeHandler.Handler.QueryEmployeesOnHotel(name, code, addr, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetEmployeesOnHotel(int id, int index, int size)
        {
            var records = 0;
            var data = Dao.EmployeeHandler.Handler.GetEmployeesOnHotel(id, index, size, out records);
            return ResultPaging(data, records);
        }

        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<object> GetQuitEmployeesOnHotel(int id, int index, int size)
        {
            var records = 0;
            var data = Dao.EmployeeHandler.Handler.GetQuitEmployeesOnHotel(id, index, size, out records);
            return ResultPaging(data, records);
        }
    }
}

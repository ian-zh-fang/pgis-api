using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class AddressController : BaseApiController
    {
        [HttpGet, HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Model.Address>> Match(string pattern)
        {
            var addresses = Dao.AddressHandler.Handler.Match(pattern);
            return ResultOk<List<Model.Address>>(addresses);
        }
    }
}

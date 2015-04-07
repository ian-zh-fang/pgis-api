using COM.TIGER.PGIS.WEBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class SysController : BaseApiController
    {
        [HttpGet,HttpPost, ActionAuthentizationFilter]
        public ApiResult<List<Menu>> GetMenu()
        {            
            var menus = Dao.MenuHandler.Handler.GetMenus();
            return Result<List<Menu>>("OK", ResultStatus.OK, menus);
        }
    }
}

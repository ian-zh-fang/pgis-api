/***********************************************************************
 * Module:  examplescontroller.cs
 * Author:  fun/方振
 * Purpose: 各模块调用示例
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COM.TIGER.PGIS.WEBAPI.Controllers
{
    public class ExampleController : Controller
    {
        //
        // GET: /Example/

        public ActionResult Index()
        {
            ViewBag.Title = "page module examples";
            return View();
        }

    }
}

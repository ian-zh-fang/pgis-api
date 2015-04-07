using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysFilter = System.Web.Http.Filters;

namespace COM.TIGER.PGIS.WEBAPI
{
    /// <summary>
    /// 操作授权过滤器
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAuthentizationFilter : SysFilter.ActionFilterAttribute
    {
        public override bool AllowMultiple { get { return false; } }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(SysFilter.HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
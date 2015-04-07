using System.Web;
using System.Web.Mvc;

namespace COM.TIGER.PGIS.WEBAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
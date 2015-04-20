using System.Web.Mvc;
using Pineapple.WebSite.Filters;

namespace Pineapple.WebSite.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new VisitorInterceptor());
        }
    }
}
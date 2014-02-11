using System.Web;
using System.Web.Mvc;

namespace Jan_die_alles_ken
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jan_die_alles_kan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Page", action = "Landingpage", id = 1 } //UrlParameter.Optional
            );

            routes.MapRoute(
                 name: "Dashboard",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Dashboard", action = "Index", id = 1 } //UrlParameter.Optional
            );

            routes.MapRoute(
                     name: "Account",
                     url: "{controller}/{action}/{id}",
                     defaults: new { controller = "Account", action = "Manage", id = 1 } //UrlParameter.Optional
            );
        }
    }
}
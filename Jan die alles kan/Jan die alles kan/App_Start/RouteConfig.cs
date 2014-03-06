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
                defaults: new { controller = "Page", action = "Details", id = 1 } //UrlParameter.Optional
            );
           
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Page/{*pathInfo}");
            routes.IgnoreRoute("UploadDownload/{*pathInfo}");
            routes.MapRoute(
               name: "dashboard",
               url: "Dashboard/{id}",
               defaults: new
               {
                   controller = "Dashboard",
                   action = "Index",
                   id = UrlParameter.Optional
               }
            );

            routes.MapRoute(
               name: "dashboardPage",
               url: "Dashboard/{controller}/{id}",
               defaults: new
               {
                   action = "Index",
                   id = UrlParameter.Optional
               }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Jan_die_alles_kan.Constraints;

namespace Jan_die_alles_kan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
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
                   controller = "Page",
                   action = "Index",
                   id = UrlParameter.Optional
               }
   );
          /*  routes.MapRoute(
             "Dashboard",
             "Dashboard/{action}/",
             new
             {
                 controller = "Dashboard",
             }
             ,
             new
             {
                 isLocal = new LocalhostConstraint()
             }
             

 );*/

            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Page", action = "Details", id = 1 } //UrlParameter.Optional
            );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
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
            //routes.MapRoute(
            //  name: "Image",
            //  url: "{controller}/{action}/{category}/{file_name}",
            //  defaults: new
            //  {
            //      controller = "Account",
            //      action = "FileDownloadPage",
            //      category = UrlParameter.Optional,
            //      file_name = UrlParameter.Optional
            //  });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Page",
                    action = "Landingpage",
                    id = UrlParameter.Optional
             });
          

        }
    }
}
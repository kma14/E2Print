using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace E2Print.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /*Controller,Action and Area are the only reserved words in asp.net MVC. 
            "Reserved" means that MVC gives special meaning to them, especially for Routing*/

            //routes.MapRoute(
            //    name: "",
            //    url: "Category/{CategoryId}",
            //    defaults: new { controller = "Category", action = "Detail" }
            //);
            routes.MapRoute(
                name: "Admin",
                url: "Admin/{action}/{id}",
                defaults: new { controller = "Admin", action = "DashBoard", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

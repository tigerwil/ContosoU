using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // routes.MapMvcAttributeRoutes();//mwilliams Attribute Routing
            //routes.MapRoute(
            //    name: "Student",
            //    url: "Student/{sortOrder}",
            //    defaults: new { controller = "Student", action = "Index" }
            //);
            //routes.MapRoute(
            //    name: "Instructor",
            //    url: "Instructor/{sortOrder}",
            //    defaults: new { controller = "Instructor", action = "Index" }
            //);
            //mwilliams:  custom route for Student/page/2
            routes.MapRoute(
               name: "Student",
               url: "Student/page/{page}",
               defaults: new { controller = "Student", action = "Index" }
           );
            routes.MapRoute(
               name: "StudentEdit",
               url: "Student/{action}/{id}/page/{page}",
               defaults: new { controller = "Student", action = "Index", page = UrlParameter.Optional }
           );
            //mwilliams: end custom routes
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

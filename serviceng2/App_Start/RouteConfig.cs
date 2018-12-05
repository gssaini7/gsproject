using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using USoftEducation.Handler;

namespace USoftEducation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "CMS",
            //    url: "{ *anything}",
            //    //url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            //);
          

            routes.MapRoute(
               name: "Admin",
               //url: "{ *anything}",
               url: "manager/{action}/{id}",
               defaults: new { controller = "Manager", action = "Index", id = UrlParameter.Optional }
           );

           // routes.MapRoute( 
           //    name: "WebPages",
           //    //url: "{ *anything}",
           //    url: "{id}",
           //    defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
           //);

            ////routes.MapRoute(
            ////    name: "Default",
            ////    //url: "{ *anything}",
            ////    url: "{controller}/{action}/{id}",
            ////    defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            ////);
            routes.MapRoute(
                name: "Default",
                //url: "{ *anything}",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Account", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "Default",
            //    //url: "{ *anything}",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}

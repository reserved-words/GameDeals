using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GameDeals.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "RssUpdateRoute",
                routeTemplate: "api/Rss/Update",
                defaults: new { controller = "Rss", action = "Update" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiNoAction",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithPaging",
                routeTemplate: "api/{controller}/{action}/{id}/{limit}/{offset}",
                defaults: new { id = RouteParameter.Optional, limit = RouteParameter.Optional, offset = RouteParameter.Optional }
            );
        }
    }
}

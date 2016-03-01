using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ubikate.Core;

namespace Ubikate
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Filters.Add(new TokenAuthenticationFilter());
            config.Filters.Add(new ExceptionLoggerFilter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProjectFlow
{
    public class WebAPIConfig
    {

        public static void Register(HttpConfiguration config)
        {

            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml")
                );

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{namespace}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional}
            );

        }

    }
}
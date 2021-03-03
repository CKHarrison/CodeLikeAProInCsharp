using System;
using System.Web.Http;

namespace FlyingDutchmanAirlinesExisting
{
    public static class WebApiConfig
    {
        public class Defaults
        {
            public readonly RouteParameter Id;

            public Defaults(RouteParameter id)
            {
                Id = id;
            }
        }

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new Defaults(RouteParameter.Optional)
            );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
                .Add(new System.Net.Http.Formatting.RequestHeaderMapping(
                    "Accept",
                    "text/html",
                    StringComparison.InvariantCultureIgnoreCase,
                    true,
                    "application/json"));
        }
    }
}

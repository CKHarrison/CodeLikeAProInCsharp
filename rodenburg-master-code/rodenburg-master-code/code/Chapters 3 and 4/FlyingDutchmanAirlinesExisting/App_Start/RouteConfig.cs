using System.Web.Mvc;
using System.Web.Routing;

namespace FlyingDutchmanAirlinesExisting
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}"
            );
        }
    }
}

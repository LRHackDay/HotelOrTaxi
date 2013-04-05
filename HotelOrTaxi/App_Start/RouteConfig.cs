using System.Web.Mvc;
using System.Web.Routing;

namespace HotelOrTaxi.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Results", "results", new { controller = "Results", action = "Index"});
            routes.MapRoute("Fight", "fight", new { controller = "Results", action = "Fight" });
            routes.MapRoute("Hotels", "hotels", new { controller = "Hotel", action = "Index" });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
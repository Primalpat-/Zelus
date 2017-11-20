using System.Web.Mvc;
using System.Web.Routing;

namespace Zelus.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Guilds",
                url: "{controller}/guilds/{swgohGgGuildId}/{action}/{id}",
                defaults: new { controller = "Api", action = "Guilds", swgohGgGuildId = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

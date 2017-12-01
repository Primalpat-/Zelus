using System.Web.Mvc;
using System.Web.Routing;

namespace Zelus.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Players",
            //    url: "api/guilds/{swgohGgGuildId}/players/{playerId}/{action}",
            //    defaults: new { action = "Collection" }
            //);

            //routes.MapRoute(
            //    name: "Guilds",
            //    url: "api/guilds/{swgohGgGuildId}/{action}/{id}",
            //    defaults: new { action = "Players", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

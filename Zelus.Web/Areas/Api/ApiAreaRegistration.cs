using System.Web.Mvc;

namespace Zelus.Web.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Player",
                url: "Api/Player/{id}/{action}",
                defaults: new { controller = "Player", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "Guild",
                url: "Api/Guild/{id}/{action}",
                defaults: new { controller = "Guild", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "Alliance",
                url: "Api/Alliance/{id}/{action}",
                defaults: new { controller = "Alliance", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
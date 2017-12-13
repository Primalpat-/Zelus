using System.Web.Mvc;

namespace Zelus.Web.ViewEngine
{
    /// <summary>
    /// This custom view engine allows us to organize our MVC members by feature.
    /// </summary>
    public class FeatureOrientedRazorViewEngine : RazorViewEngine
    {
        public FeatureOrientedRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
                 {
                 "~/Areas/{2}/Controllers/{1}/Views/{0}.cshtml",
                 "~/Areas/{2}/Controllers/Shared/Views/{0}.cshtml",
                 "~/Areas/Shared/Views/{0}.cshtml"
                 };
            AreaMasterLocationFormats = new[]
                 {
                 "~/Areas/{2}/Controllers/{1}/Views/{0}.cshtml",
                 "~/Areas/{2}/Controllers/Shared/Views/{0}.cshtml",
                 "~/Areas/Shared/Views/{0}.cshtml"
                 };
            AreaPartialViewLocationFormats = new[]
                 {
                 "~/Areas/{2}/Controllers/{1}/Views/{0}.cshtml",
                 "~/Areas/{2}/Controllers/Shared/Views/{0}.cshtml",
                 "~/Areas/Shared/Views/{0}.cshtml"
                 };
            ViewLocationFormats = new[]
                 {
                 "~/Controllers/{1}/Views/{0}.cshtml",
                 "~/Controllers/Shared/Views/{0}.cshtml"
                 };
            MasterLocationFormats = new[]
                 {
                 "~/Controllers/{1}/Views/{0}.cshtml",
                 "~/Controllers/Shared/Views/{0}.cshtml"
                 };
            PartialViewLocationFormats = new[]
                {
                 "~/Controllers/{1}/Views/{0}.cshtml",
                 "~/Controllers/Shared/Views/{0}.cshtml"
                };
        }
    }
}

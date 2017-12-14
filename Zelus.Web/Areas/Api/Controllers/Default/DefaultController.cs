using System.Web.Mvc;

namespace Zelus.Web.Areas.Api.Controllers.Default
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
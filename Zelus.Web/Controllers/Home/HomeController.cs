using System.Web.Mvc;

namespace Zelus.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Home.");
        }
    }
}
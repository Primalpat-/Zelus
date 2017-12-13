using System.Web.Mvc;

namespace Zelus.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Home.");
        }
    }
}
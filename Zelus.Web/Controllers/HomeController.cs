using System.Web.Mvc;
using Zelus.Web.Models.Synchronizers;

namespace Zelus.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SyncStuff()
        {
            var unitSync = new UnitSynchronizer();
            unitSync.Execute();

            var playerSync = new PlayerSynchronizer();
            playerSync.Execute();

            return View("Index");
        }







        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
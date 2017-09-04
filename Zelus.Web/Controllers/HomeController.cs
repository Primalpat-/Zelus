using System.Web.Mvc;
using Zelus.Web.Models.Synchronization;

namespace Zelus.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string guildUrl)
        {
            Synchronizer.ExecuteForGuild(guildUrl);

            return View();
        }
    }
}
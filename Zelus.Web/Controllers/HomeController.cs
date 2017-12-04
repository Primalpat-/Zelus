using System.Linq;
using System.Web.Mvc;
using Zelus.Data;

namespace Zelus.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Home.");
        }

        public ActionResult Mods()
        {
            //var db = new ZelusDbContext();

            //var mode = db.PlayerMods
            //             .Where(m => m.SetId == (int)ModSets.Crit_Chance || m.SetId == (int)ModSets.Crit_Damage)

            return View();
        }
    }
}
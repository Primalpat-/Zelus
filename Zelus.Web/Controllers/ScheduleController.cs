using System.Web.Mvc;
using Zelus.Web.Models.Synchronizers;

namespace Zelus.Web.Controllers
{
    public class ScheduleController : Controller
    {
        public ActionResult SwgohGgSync()
        {
            var unitSync = new UnitSynchronizer();
            unitSync.Execute();

            var playerSync = new PlayerSynchronizer();
            playerSync.Execute();

            var collectionSync = new CollectionSynchronizer();
            collectionSync.Execute();

            return Content("Sync Complete.");
        }
    }
}
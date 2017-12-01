using System.Web.Mvc;
using Zelus.Web.Models;
using Zelus.Web.Models.Synchronization;

namespace Zelus.Web.Controllers
{
    public class ScheduleController : Controller
    {
        public ActionResult SwgohGgSync()
        {
            var synchronizer = new Synchronizer();
            var syncOutcome = synchronizer.Execute();

            return Content(string.Join("\n", syncOutcome.Messages));
        }
    }
}
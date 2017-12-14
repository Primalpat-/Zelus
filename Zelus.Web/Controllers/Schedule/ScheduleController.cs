using System.Web.Mvc;
using Zelus.Logic.Synchronization;

namespace Zelus.Web.Controllers.Schedule
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
using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Z.Core.Extensions;
using Z.Reflection.Extensions;
using Zelus.Data.Models;
using Zelus.Web.Models.Helpers;

namespace Zelus.Web.Controllers
{
    public class DataApiController : Controller
    {
        public ActionResult GetRaids([DataSourceRequest] DataSourceRequest request)
        {
            var db = new ZelusContext();
            var raids = db.Raids
                          .OrderBy(r => r.SortOrder)
                          .Select(r => new
                          {
                              RaidId = r.Id,
                              RaidName = r.Name
                          })
                          .ToList();
            return Json(raids, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRaidPhases([DataSourceRequest] DataSourceRequest request)
        {
            var raidId = request.Filters[0].GetPropertyValue("Value").ToInt32();

            var db = new ZelusContext();
            var phases = db.Raids
                           .Single(r => r.Id == raidId)
                           .RaidPhases
                           .Select(p => new
                           {
                               PhaseId = p.Id,
                               PhaseName = $"{p.Name} ({p.Health.ToHumanReadable()} hp)"
                           })
                           .ToList();
            return Json(phases, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlayerCharacters([DataSourceRequest] DataSourceRequest request, string playerUsername)
        {
            //var name = request.Filters[0].GetPropertyValue("Value").ToString();

            var db = new ZelusContext();
            var playerCharacters = db.PlayerCharacters
                                     .Where(pc => pc.Player.Name.ToLower() == playerUsername.ToLower())
                                     .Select(pc => new
                                     {
                                         Id = pc.Id,
                                         Name = pc.Character.Name
                                     });
            var result = playerCharacters.ToDataSourceResult(request).Data;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
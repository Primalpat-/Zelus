using System;
using System.Linq;
using System.Web.Mvc;
using Ether.Outcomes;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Logic.Services.TerritoryWarPlanning;
using Zelus.Logic.Services.TerritoryWarPlanning.Strategy;
using Zelus.Web.Controllers.TerritoryWar.Models;

namespace Zelus.Web.Controllers.TerritoryWar
{
    public class TerritoryWarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Strategy()
        {
            var model = new StrategyViewModel();
            return View(model);
        }

        public ActionResult CalculateStrategyForPlayer(int strategyType, string playerName)
        {
            if (strategyType.IsDefault())
                return Json(Outcomes.Failure().WithMessage("You cannot go into battle without a proper strategy!"), JsonRequestBehavior.AllowGet);

            var db = new ZelusDbContext();
            var player = db.Players.FirstOrDefault(p => string.Compare(p.SwgohGgName, playerName, StringComparison.OrdinalIgnoreCase) == 0);
            if (player.IsNull())
                return Json(Outcomes.Failure().WithMessage("We cannot find a pawn-Err ..ahem, loyal soldier with that name."), JsonRequestBehavior.AllowGet);

            var type = (StrategyType)strategyType;
            var strategy = GetStrategyFromType(type);
            var planner = new PlanningService();
            var planOutcome = planner.Execute(strategy, db, player);

            return Json(planOutcome, JsonRequestBehavior.AllowGet);
        }

        private PlanningStrategyBase GetStrategyFromType(StrategyType type)
        {
            switch (type)
            {
                case StrategyType.Defensive:
                    return new DefensiveStrategy();
                case StrategyType.Balanced:
                    return new BalancedStrategy();
                case StrategyType.Aggressive:
                    return new AggressiveStrategy();
                default:
                    throw new Exception("Unrecognized strategy type.");
            }
        }
    }
}
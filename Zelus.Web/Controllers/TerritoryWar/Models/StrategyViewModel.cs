using System.ComponentModel.DataAnnotations;
using Zelus.Logic.Services.TerritoryWarPlanning.Strategy;

namespace Zelus.Web.Controllers.TerritoryWar.Models
{
    public class StrategyViewModel
    {
        public StrategyViewModel()
        {
            Strategy = new StrategyResultViewModel();
        }

        [Required]
        public int StrategyType { get; set; }

        [Required]
        public string PlayerName { get; set; }

        public StrategyResultViewModel Strategy { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using Zelus.Web.Models.Views.TerritoryWar.Planning;

namespace Zelus.Web.Models.Views.TerritoryWar
{
    public class StrategyViewModel
    {
        public StrategyViewModel()
        {
            Strategy = new StrategyResultVM();
        }

        [Required]
        public int StrategyType { get; set; }

        [Required]
        public string PlayerName { get; set; }

        public StrategyResultVM Strategy { get; set; }
    }
}
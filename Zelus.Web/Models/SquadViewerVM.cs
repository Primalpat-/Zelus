using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zelus.Web.Models
{
    public class SquadViewerVM
    {
        [UIHint("RaidId")]
        [DisplayName("Raid")]
        public int RaidId { get; set; }

        public string RaidName { get; set; }

        [UIHint("PhaseId")]
        [DisplayName("Phase")]
        public int PhaseId { get; set; }

        public string PhaseName { get; set; }
    }
}
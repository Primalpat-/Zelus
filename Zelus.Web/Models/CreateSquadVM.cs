using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zelus.Web.Models
{
    public class CreateSquadVM
    {
        [DisplayName("Raid")]
        public int RaidId { get; set; }
        [DisplayName("Phase")]
        public int PhaseId { get; set; }
        public string Name { get; set; }
        public string VictoryScreenUrl { get; set; }
        public int Member1Id { get; set; }
        public int Member2Id { get; set; }
        public int Member3Id { get; set; }
        public int Member4Id { get; set; }
        public int Member5Id { get; set; }
        public int Damage { get; set; }
    }
}
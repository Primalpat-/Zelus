using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zelus.Data.Models;

namespace Zelus.Web.Models
{
    public class CreateSquadVM
    {
        public string PlayerUsername { get; set; }
        [Required]
        [DisplayName("Raid")]
        public int RaidId { get; set; }
        [Required]
        [DisplayName("Phase")]
        public int PhaseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Victory screen url")]
        public string VictoryScreenUrl { get; set; }
        [Required]
        [DisplayName("Leader")]
        public int Member1Id { get; set; }
        [DisplayName("Member")]
        public int Member2Id { get; set; }
        [DisplayName("Member")]
        public int Member3Id { get; set; }
        [DisplayName("Member")]
        public int Member4Id { get; set; }
        [DisplayName("Member")]
        public int Member5Id { get; set; }
        [Required]
        public int Damage { get; set; }
        public string Notes { get; set; }
        public List<PlayerCharacter> PlayerCharacters { get; set; }
    }
}
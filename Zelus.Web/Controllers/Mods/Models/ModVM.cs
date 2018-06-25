using Zelus.Data;

namespace Zelus.Web.Controllers.Mods.Models
{
    public class ModVM
    {
        public bool IsInModSet { get; set; }
        public bool ShowCheckbox { get; set; }
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public int Pips { get; set; }

        public ModSets Set { get; set; }
        public ModSlots Slot { get; set; }
        public string ModImg { get; set; }

        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string CharacterUrl { get; set; }
        public string CharacterImg { get; set; }

        public int PrimaryType { get; set; }
        public string Primary { get; set; }
        public string Secondary1 { get; set; }
        public string Secondary2 { get; set; }
        public string Secondary3 { get; set; }
        public string Secondary4 { get; set; }

        public decimal Speed { get; set; }
        public decimal CritChance { get; set; }
        public decimal CritDamage { get; set; }
        public decimal FlatOffense { get; set; }
        public decimal PercentageOffense { get; set; }
        public decimal Potency { get; set; }
        public decimal Accuracy { get; set; }

        public decimal FlatProtection { get; set; }
        public decimal PercentageProtection { get; set; }
        public decimal FlatHealth { get; set; }
        public decimal PercentageHealth { get; set; }
        public decimal FlatDefense { get; set; }
        public decimal PercentageDefense { get; set; }
        public decimal Tenacity { get; set; }
        public decimal CritAvoidance { get; set; }
    }
}
namespace Zelus.Web.Controllers.Mods.Models
{
    public class ModSetVM
    {
        public int Id { get; set; }

        public ModVM Square { get; set; }
        public ModVM Arrow { get; set; }
        public ModVM Diamond { get; set; }
        public ModVM Triangle { get; set; }
        public ModVM Circle { get; set; }
        public ModVM Cross { get; set; }

        public string TotalSpeed { get; set; }
        public string TotalCritChance { get; set; }
        public string TotalCritDamage { get; set; }
        public string TotalFlatOffense { get; set; }
        public string TotalPercentageOffense { get; set; }
        public string TotalPotency { get; set; }
        public string TotalAccuracy { get; set; }

        public string TotalFlatProtection { get; set; }
        public string TotalPercentageProtection { get; set; }
        public string TotalFlatHealth { get; set; }
        public string TotalPercentageHealth { get; set; }
        public string TotalFlatDefense { get; set; }
        public string TotalPercentageDefense { get; set; }
        public string TotalTenactiy { get; set; }
        public string TotalCritAvoid { get; set; }
    }
}
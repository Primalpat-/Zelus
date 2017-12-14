using System;

namespace Zelus.Web.Controllers.Mods.Models
{
    public class ModPlannerVM
    {
        public int PlayerId { get; set; }
        public string LastSyncHumanized { get; set; }
        public DateTime LastSyncDateTime { get; set; }
    }
}
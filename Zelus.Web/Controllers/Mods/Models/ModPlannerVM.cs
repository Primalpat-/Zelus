using System;
using System.Collections.Generic;

namespace Zelus.Web.Controllers.Mods.Models
{
    public class ModPlannerVM
    {
        public ModPlannerVM()
        {
            Mods = new List<ModVM>();    
        }

        public int PlayerId { get; set; }
        public string LastSyncHumanized { get; set; }
        public DateTime LastSyncDateTime { get; set; }
        public List<ModVM> Mods { get; set; }
        public List<CharacterVM> Characters { get; set; }
    }
}
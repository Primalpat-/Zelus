using System.Collections.Generic;
using System.Linq;
using Zelus.Data.Models;

namespace Zelus.Web.Models.Extensions
{
    public static class RaidExtensions
    {
        public static List<Squad> PhaseOneSquads(this Raid raid, int count)
        {
            return raid.RaidPhases
                       .First(p => p.Name == "Phase 1")
                       .Squads
                       .OrderByDescending(s => s.Damage)
                       .Take(count)
                       .ToList();
        }

        public static List<Squad> PhaseTwoSquads(this Raid raid, int count)
        {
            return raid.RaidPhases
                       .First(p => p.Name == "Phase 2")
                       .Squads
                       .OrderByDescending(s => s.Damage)
                       .Take(count)
                       .ToList();
        }

        public static List<Squad> PhaseThreeSquads(this Raid raid, int count)
        {
            return raid.RaidPhases
                       .First(p => p.Name == "Phase 3")
                       .Squads
                       .OrderByDescending(s => s.Damage)
                       .Take(count)
                       .ToList();
        }

        public static List<Squad> PhaseFourSquads(this Raid raid, int count)
        {
            return raid.RaidPhases
                       .First(p => p.Name == "Phase 4")
                       .Squads
                       .OrderByDescending(s => s.Damage)
                       .Take(count)
                       .ToList();
        }
    }
}
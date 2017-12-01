using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zelus.Data;

namespace Zelus.Web.Models.DataSimplification
{
    public static class PlayerCharacterSimplifier
    {
        public static object SimplePlayerCharacter(this PlayerCharacter pc)
        {
            return new
            {
                UnitName = pc.Unit.Name,
                UnitBaseId = pc.Unit.BaseId,
                UnitMaxPower = pc.Unit.Power,
                CharacterGear = pc.Gear,
                CharacterLevel = pc.Level,
                CharacterStars = pc.Stars,
                CharacterPower = pc.Power
            };
        }
    }
}
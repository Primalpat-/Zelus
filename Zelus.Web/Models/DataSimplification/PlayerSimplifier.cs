using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zelus.Data;

namespace Zelus.Web.Models.DataSimplification
{
    public static class PlayerSimplifier
    {
        public static object SimplePlayer(this Player player)
        {
            return new
            {
                PlayerId = player.Id,
                player.InGameName,
                player.SwgohGgName,
                player.SwgohGgUrl,
                player.LastCollectionSync
            };
        }

        public static object SimplePlayerWithCollection(this Player player)
        {
            return new
            {
                PlayerId = player.Id,
                player.InGameName,
                player.SwgohGgName,
                player.SwgohGgUrl,
                player.LastCollectionSync,
                Collection = player.PlayerCharacters
                                   .Select(PlayerCharacterSimplifier.SimplePlayerCharacter)
                                   .ToList()
            };
        }
    }
}
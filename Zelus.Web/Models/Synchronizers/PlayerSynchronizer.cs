using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.BulkExtensions.Operations;
using Ether.Outcomes;
using HtmlAgilityPack;
using Zelus.Data;
using Zelus.Web.Helpers.GuildHome;

namespace Zelus.Web.Models.Synchronizers
{
    public class PlayerSynchronizer
    {
        private readonly ZelusDbContext _db;
        private readonly List<Player> _players;

        private List<Player> _newPlayers = new List<Player>();
        private List<Player> _playersToUpdate = new List<Player>();
        private List<Player> _playersToRemove = new List<Player>();

        public IOutcome Execute()
        {
            try
            {
                CategorizePlayers();

                if (_newPlayers.Count > 0)
                    _db.BulkInsert(_newPlayers);

                if (_playersToUpdate.Count > 0)
                    _db.BulkUpdate(_playersToUpdate);

                if (_playersToRemove.Count > 0)
                    _db.BulkDelete(_playersToRemove);

                _db.SaveChanges();

                return Outcomes.Success();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure()
                               .WithMessage(ex.ToString());
            }
        }

        private void CategorizePlayers()
        {
            var web = new HtmlWeb();
            var guilds = _db.Guilds.ToList();

            foreach (var guild in guilds)
            {
                
                var guildHome = web.Load(guild.SwgohGgUrl);
                var playerRows = guildHome.DocumentNode
                                          .Descendants("tbody")
                                          .First()
                                          .Descendants("tr")
                                          .ToList();

                foreach (var row in playerRows)
                {
                    var player = row.Descendants("td").ParsePlayer(guild.Id);
                    var savedPlayer = _players.FirstOrDefault(p => string.Compare(p.SwgohGgUrl, 
                                                                                  player.SwgohGgUrl,
                                                                                  StringComparison.OrdinalIgnoreCase) == 0);
                    if (savedPlayer == null)
                        _newPlayers.Add(player);
                    else
                    {
                        savedPlayer.GuildId = guild.Id;
                        savedPlayer.InGameName = player.InGameName;
                        savedPlayer.SwgohGgName = player.SwgohGgName;
                        savedPlayer.SwgohGgUrl = player.SwgohGgUrl;

                        _playersToUpdate.Add(savedPlayer);
                    }
                }

                foreach (var savedPlayer in _players)
                {
                    var updatedPlayer = _playersToUpdate.FirstOrDefault(p => string.Compare(p.SwgohGgUrl,
                                                                                            savedPlayer.SwgohGgUrl,
                                                                                            StringComparison.OrdinalIgnoreCase) == 0);
                    if (updatedPlayer == null)
                        _playersToRemove.Add(savedPlayer);
                }
            }
        }

        public PlayerSynchronizer()
        {
            _db = new ZelusDbContext();
            _players = _db.Players.ToList();
        }
    }
}
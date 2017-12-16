using System;
using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Logic.Synchronization.Scrapers;

namespace Zelus.Logic.Synchronization.Synchronizers
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
            var scraper = new PlayerScraper();
            var remotePlayers = scraper.Execute(_db);

            foreach (var remotePlayer in remotePlayers)
            {
                var savedPlayer = _players.FirstOrDefault(p => string.Compare(p.SwgohGgUrl,
                                                                              remotePlayer.SwgohGgUrl,
                                                                              StringComparison.OrdinalIgnoreCase) == 0);
                if (savedPlayer == null)
                    _newPlayers.Add(remotePlayer);
                else
                {
                    savedPlayer.GuildId = remotePlayer.GuildId;
                    savedPlayer.InGameName = remotePlayer.InGameName;
                    savedPlayer.SwgohGgName = remotePlayer.SwgohGgName;
                    savedPlayer.SwgohGgUrl = remotePlayer.SwgohGgUrl;

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

        public PlayerSynchronizer()
        {
            _db = new ZelusDbContext();
            _players = _db.Players.ToList();
        }
    }
}
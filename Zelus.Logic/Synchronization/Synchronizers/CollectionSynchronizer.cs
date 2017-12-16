using System;
using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Logic.Synchronization.Scrapers;

namespace Zelus.Logic.Synchronization.Synchronizers
{
    public class CollectionSynchronizer
    {
        private readonly ZelusDbContext _db;
        private readonly List<Player> _players;
        private readonly List<PlayerCharacter> _characters;

        private List<Player> _playersToSync = new List<Player>();
        private List<PlayerCharacter> _newPlayerCharacters = new List<PlayerCharacter>();
        private List<PlayerCharacter> _playerCharactersToUpdate = new List<PlayerCharacter>();

        public IOutcome Execute()
        {
            try
            {
                CategorizePlayerCharacters();

                if (_newPlayerCharacters.Count > 0)
                    _db.BulkInsert(_newPlayerCharacters);

                if (_playerCharactersToUpdate.Count > 0)
                    _db.BulkUpdate(_playerCharactersToUpdate);

                if (_playersToSync.Count > 0)
                    _db.BulkUpdate(_playersToSync);

                return Outcomes.Success();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure()
                               .WithMessage(ex.ToString());
            }
        }

        private void CategorizePlayerCharacters()
        {
            var scraper = new CollectionScraper();
            var remotePlayerCharacters = scraper.Execute(_db);

            BuildPlayersToSync(remotePlayerCharacters);

            foreach (var remotePlayerCharacter in remotePlayerCharacters)
            {
                var savedPlayerCharacter = _characters.FirstOrDefault(c => c.PlayerId == remotePlayerCharacter.PlayerId &&
                                                                           c.UnitId == remotePlayerCharacter.UnitId);

                if (savedPlayerCharacter == null)
                    _newPlayerCharacters.Add(remotePlayerCharacter);
                else
                {
                    savedPlayerCharacter.Gear = remotePlayerCharacter.Gear;
                    savedPlayerCharacter.Level = remotePlayerCharacter.Level;
                    savedPlayerCharacter.Power = remotePlayerCharacter.Power;
                    savedPlayerCharacter.Stars = remotePlayerCharacter.Stars;

                    _playerCharactersToUpdate.Add(savedPlayerCharacter);
                }
            }
        }

        private void BuildPlayersToSync(List<PlayerCharacter> remotePlayerCharacters)
        {
            var playerIds = remotePlayerCharacters.Select(pc => pc.PlayerId).Distinct().ToList();
            foreach (var playerId in playerIds)
            {
                var player = _playersToSync.FirstOrDefault(p => p.Id == playerId);

                if (player.IsNotNull())
                    continue;

                player = _players.Single(p => p.Id == playerId);
                player.LastCollectionSync = DateTime.UtcNow;
                _playersToSync.Add(player);
            }
        }

        public CollectionSynchronizer()
        {
            _db = new ZelusDbContext();
            _players = _db.Players.Where(p => p.CollectionSyncEnabled).ToList();
            _characters = _db.PlayerCharacters.ToList();
        }
    }
}
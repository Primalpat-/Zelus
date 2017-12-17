using System;
using System.Collections.Generic;
using System.Linq;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Logic.Synchronization.Scrapers;
using System.Data.Entity.Migrations;

namespace Zelus.Logic.Synchronization.Synchronizers
{
    public class CollectionSynchronizer
    {
        private readonly ZelusDbContext _db;
        private readonly List<Player> _players;
        private readonly List<PlayerCharacter> _characters;

        private List<PlayerCharacter> _newPlayerCharacters = new List<PlayerCharacter>();
        private List<PlayerCharacter> _playerCharactersToUpdate = new List<PlayerCharacter>();

        public IOutcome Execute()
        {
            try
            {
                CategorizePlayerCharacters();

                if (_newPlayerCharacters.Count > 0)
                    _db.PlayerCharacters.AddRange(_newPlayerCharacters);

                foreach (var playerCharacterToUpdate in _playerCharactersToUpdate)
                    _db.PlayerCharacters.AddOrUpdate(playerCharacterToUpdate);

                foreach (var playerToSync in _players)
                {
                    playerToSync.LastCollectionSync = DateTime.UtcNow;
                    _db.Players.AddOrUpdate(playerToSync);
                } 

                _db.SaveChanges();

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
            var remotePlayerCharacters = scraper.Execute(_db, _players);

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

        public CollectionSynchronizer()
        {
            _db = new ZelusDbContext();

            var timeFilter = DateTime.UtcNow.AddHours(-18);
            _players = _db.Players.Where(p => p.CollectionSyncEnabled &&
                                                p.LastCollectionSync < timeFilter)
                                  .OrderBy(p => p.LastCollectionSync)
                                  .ToList();

            _characters = _players.SelectMany(p => p.PlayerCharacters)
                                  .ToList();
        }
    }
}
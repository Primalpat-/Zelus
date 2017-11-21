using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.BulkExtensions.Operations;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Web.Models.Scrapers;

namespace Zelus.Web.Models.Synchronizers
{
    public class CollectionSynchronizer
    {
        private readonly ZelusDbContext _db;
        private readonly List<PlayerCharacter> _characters;

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
            var remotePlayerCharacters = scraper.Execute(_db);

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
            _characters = _db.PlayerCharacters.ToList();
        }
    }
}
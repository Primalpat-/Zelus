using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Ether.Outcomes;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Logic.Synchronization.Scrapers;

namespace Zelus.Logic.Synchronization.Synchronizers
{
    public class ModSynchronizer
    {
        private readonly ZelusDbContext _db;
        private readonly List<Player> _players;
        private readonly List<PlayerMod> _mods;
        
        private List<PlayerMod> _newMods = new List<PlayerMod>();
        private List<PlayerMod> _modsToUpdate = new List<PlayerMod>();

        public IOutcome Execute()
        {
            try
            {
                CategorizeMods();
                
                var pcRemovedMods = _mods.Select(m => { m.PlayerCharacter = null; m.PlayerCharacterId = null; return m; });
                foreach(var removedMod in pcRemovedMods)
                    _db.PlayerMods.AddOrUpdate(removedMod);

                if (_newMods.Count > 0)
                    _db.PlayerMods.AddRange(_newMods);

                foreach (var modToUpdate in _modsToUpdate)
                    _db.PlayerMods.AddOrUpdate(modToUpdate);

                foreach (var player in _players)
                {
                    player.LastModSync = DateTime.UtcNow;
                    _db.Players.AddOrUpdate(player);
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

        public void CategorizeMods()
        {
            var scraper = new ModScraper();
            var remoteMods = scraper.Execute(_db, _players);

            foreach (var remoteMod in remoteMods)
            {
                var savedMod = LookupSavedMod(remoteMod);

                if (savedMod == null)
                    _newMods.Add(remoteMod);
                else
                {
                    savedMod.SwgohGgId = remoteMod.SwgohGgId;
                    savedMod.PlayerCharacterId = remoteMod.PlayerCharacterId;
                    savedMod.PrimaryValue = remoteMod.PrimaryValue;
                    savedMod.Secondary1Value = remoteMod.Secondary1Value;
                    savedMod.Secondary2Value = remoteMod.Secondary2Value;
                    savedMod.Secondary3Value = remoteMod.Secondary3Value;
                    savedMod.Secondary4Value = remoteMod.Secondary4Value;

                    _modsToUpdate.Add(savedMod);
                }
            }
        }

        private PlayerMod LookupSavedMod(PlayerMod remoteMod)
        {
            var savedMod = _mods.FirstOrDefault(m => m.SwgohGgId == remoteMod.SwgohGgId);

            if (savedMod.IsNotNull())
                _mods.Remove(savedMod);

            return savedMod;
        }

        public ModSynchronizer()
        {
            _db = new ZelusDbContext();

            var timeFilter = DateTime.UtcNow.AddHours(-18);
            _players = _db.Players.Where(p => p.ModSyncEnabled &&
                                              p.LastModSync < timeFilter)
                                  .Include(p => p.PlayerCharacters)
                                  .OrderBy(p => p.LastModSync)
                                  .Take(2)
                                  .ToList();

            _mods = _players.SelectMany(p => p.PlayerMods)
                            .ToList();
        }
    }
}
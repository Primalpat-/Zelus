using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFramework.BulkExtensions.Operations;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Web.Models.Synchronization.Scrapers;
using Z.Core.Extensions;

namespace Zelus.Web.Models.Synchronization.Synchronizers
{
    public class ModSynchronizer
    {
        private readonly ZelusDbContext _db;
        private readonly List<Player> _players;
        private readonly List<PlayerMod> _mods;

        private List<Player> _playersToSync = new List<Player>();
        private List<PlayerMod> _newMods = new List<PlayerMod>();
        private List<PlayerMod> _modsToUpdate = new List<PlayerMod>();

        public IOutcome Execute()
        {
            try
            {
                CategorizeMods();

                if (_newMods.Count > 0)
                    _db.BulkInsert(_newMods);

                if (_modsToUpdate.Count > 0)
                    _db.BulkUpdate(_modsToUpdate);

                if (_mods.Count > 0)
                {
                    var pcRemovedMods = _mods.Select(m => { m.PlayerCharacter = null; m.PlayerCharacterId = null; return m; });
                    _db.BulkUpdate(pcRemovedMods);
                }

                if (_playersToSync.Count > 0)
                    _db.BulkUpdate(_playersToSync);

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
            var remoteMods = scraper.Execute(_db);

            BuildPlayersToSync(remoteMods);

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

        private void BuildPlayersToSync(List<PlayerMod> remotePlayerMods)
        {
            var playerIds = remotePlayerMods.Select(pc => pc.PlayerId).Distinct().ToList();
            foreach (var playerId in playerIds)
            {
                var player = _playersToSync.FirstOrDefault(p => p.Id == playerId);

                if (player.IsNotNull())
                    continue;

                player = _players.Single(p => p.Id == playerId);
                player.LastModSync = DateTime.UtcNow;
                _playersToSync.Add(player);
            }
        }

        private PlayerMod LookupSavedMod(PlayerMod remoteMod)
        {
            var savedMod = _mods.FirstOrDefault(m => m.SwgohGgId == remoteMod.SwgohGgId);

            if (savedMod.IsNull())
                savedMod = _mods.FirstOrDefault(m => m.PlayerId == remoteMod.PlayerId &&
                                                     m.SlotId == remoteMod.SlotId &&
                                                     m.SetId == remoteMod.SetId &&
                                                     m.Pips == remoteMod.Pips &&
                                                     m.PrimaryValue == remoteMod.PrimaryValue &&
                                                     m.Secondary1Value == remoteMod.Secondary1Value &&
                                                     m.Secondary2Value == remoteMod.Secondary2Value &&
                                                     m.Secondary3Value == remoteMod.Secondary3Value &&
                                                     m.Secondary4Value == remoteMod.Secondary4Value);

            if (savedMod.IsNotNull())
                _mods.Remove(savedMod);

            return savedMod;
        }

        public ModSynchronizer()
        {
            _db = new ZelusDbContext();
            _players = _db.Players.Where(p => p.ModSyncEnabled).ToList();
            _mods = _db.PlayerMods.ToList();
        }
    }
}
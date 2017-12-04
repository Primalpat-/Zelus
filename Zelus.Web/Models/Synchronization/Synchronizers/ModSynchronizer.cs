using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFramework.BulkExtensions.Operations;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Web.Models.Synchronization.Scrapers;

namespace Zelus.Web.Models.Synchronization.Synchronizers
{
    public class ModSynchronizer
    {
        private readonly ZelusDbContext _db;
        private readonly List<PlayerMod> _mods;

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

            foreach (var remoteMod in remoteMods)
            {
                var savedMod = _mods.FirstOrDefault(m => m.PlayerId == remoteMod.PlayerId &&
                                                         m.SlotId == remoteMod.SlotId &&
                                                         m.SetId == remoteMod.SetId &&
                                                         m.Pips == remoteMod.Pips &&
                                                         m.PrimaryValue == remoteMod.PrimaryValue &&
                                                         m.Secondary1Value == remoteMod.Secondary1Value &&
                                                         m.Secondary2Value == remoteMod.Secondary2Value &&
                                                         m.Secondary3Value == remoteMod.Secondary3Value &&
                                                         m.Secondary4Value == remoteMod.Secondary4Value);

                if (savedMod == null)
                    _newMods.Add(remoteMod);
                else
                {
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

        public ModSynchronizer()
        {
            _db = new ZelusDbContext();
            _mods = _db.PlayerMods.ToList();
        }
    }
}
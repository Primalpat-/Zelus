using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ether.Outcomes;
using Zelus.Data;
using Zelus.Web.Models.Synchronization.Scrapers;

namespace Zelus.Web.Models.Synchronization.Synchronizers
{
    public class ModSynchronizer
    {
        private readonly ZelusDbContext _db;

        private List<PlayerMod> _newMods = new List<PlayerMod>();
        private List<PlayerMod> _modsToUpdate = new List<PlayerMod>();

        public IOutcome Execute()
        {
            CategorizeMods();


        }

        public void CategorizeMods()
        {
            var scraper = new ModScraper();
            var remoteMods = scraper.Execute(_db);
        }

        public ModSynchronizer()
        {
            _db = new ZelusDbContext();
        }
    }
}
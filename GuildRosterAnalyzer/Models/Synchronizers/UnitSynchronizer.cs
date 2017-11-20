﻿using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.BulkExtensions.Operations;
using Ether.Outcomes;
using GuildRosterAnalyzer.Data;
using GuildRosterAnalyzer.Web.Models.API;
using RestSharp;

namespace GuildRosterAnalyzer.Web.Models.Synchronizers
{
    public class UnitSynchronizer
    {
    //    private const string CharacterUrl = "https://swgoh.gg/api/characters/?format=json";
    //    private const string ShipUrl = "https://swgoh.gg/api/ships/?format=json";
        private readonly SwgohGgApi _api;
        private readonly GraDbContext _db;
        private readonly List<Unit> _units;

        private List<Unit> _newUnits = new List<Unit>();
        private List<Unit> _unitsToUpdate = new List<Unit>();

        public IOutcome Execute()
        {
            try
            {
                CategorizeUnits();

                if (_newUnits.Count > 0)
                    _db.BulkInsert(_newUnits);

                if (_unitsToUpdate.Count > 0)
                    _db.BulkUpdate(_unitsToUpdate);

                _db.SaveChanges();

                return Outcomes.Success();
            }
            catch (Exception ex)
            {
                return Outcomes.Failure()
                               .WithMessage(ex.ToString());
            }
        }

        private void CategorizeUnits()
        {
            var remoteUnits = GetRemoteUnits();

            foreach (var remoteUnit in remoteUnits)
            {
                var savedUnit = _units.FirstOrDefault(u => u.BaseId == remoteUnit.BaseId);
                if (savedUnit == null)
                    _newUnits.Add(remoteUnit);
                else
                {
                    savedUnit.CombatType = remoteUnit.CombatType;
                    savedUnit.Description = remoteUnit.Description;
                    savedUnit.Image = remoteUnit.Image;
                    savedUnit.Name = remoteUnit.Name;
                    savedUnit.Power = remoteUnit.Power;
                    savedUnit.Url = remoteUnit.Url;

                    _unitsToUpdate.Add(savedUnit);
                }
            }
        }

        private List<Unit> GetRemoteUnits()
        {
            var characters = GetCharacters();
            var ships = GetShips();
            return characters.Concat(ships).ToList();
        }

        private List<Unit> GetCharacters()
        {
            var request = new RestRequest();
            request.Resource = "characters/?format=json";
            request.RootElement = "Unit";

            return _api.Execute<List<Unit>>(request);
        }

        private List<Unit> GetShips()
        {
            var request = new RestRequest();
            request.Resource = "ships/?format=json";
            request.RootElement = "Unit";

            return _api.Execute<List<Unit>>(request);
        }

        public UnitSynchronizer()
        {
            _api = new SwgohGgApi();
            _db = new GraDbContext();
            _units = _db.Units.ToList();
        }
    }
}
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Ether.Outcomes;
using HtmlAgilityPack;
using Z.Core.Extensions;
using Zelus.Data.Models;

namespace Zelus.Web.Models.Synchronization
{
    public static class GuildSynchronizer
    {
        private static ZelusContext _db;

        /// <summary>
        /// Using the provided guild's url, this function will create a new guild in our database
        /// if one does not already exist with the same url, and then it will create a new synchronization
        /// in our database if it follows the synchronization rules.
        /// </summary>
        public static IOutcome<Guild> Execute(Guild guild, string guildUrl)
        {
            try
            {
                _db = new ZelusContext();

                var web = new HtmlWeb();
                var doc = web.Load(guildUrl);

                var guildOutcome = CreateOrUpdateGuild(doc, guild, guildUrl);
                if (guildOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                    .WithMessagesFrom(guildOutcome);

                guild = guildOutcome.Value;

                var syncOutcome = CreateNewSynchronization(doc, guild);
                if (syncOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                   .WithMessagesFrom(syncOutcome);

                return Outcomes.Success<Guild>()
                               .WithValue(guild);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Guild>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<GuildSynchronization> CreateNewSynchronization(HtmlDocument doc, Guild guild)
        {
            try
            {
                var guildData = doc.DocumentNode.OuterHtml.Replace("\n", "");

                var sync = new GuildSynchronization
                {
                    GuildId = guild.Id,
                    Timestamp = DateTime.UtcNow,
                    Data = guildData
                };

                _db.GuildSynchronizations.Add(sync);
                _db.SaveChanges();

                return Outcomes.Success<GuildSynchronization>()
                               .WithValue(sync);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<GuildSynchronization>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<Guild> CreateOrUpdateGuild(HtmlDocument doc, Guild guild, string guildUrl)
        {
            try
            {
                var nameOutcome = ParseGuildName(doc);
                if (nameOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                   .WithMessagesFrom(nameOutcome);

                if (guild.IsNull())
                {
                    guild = new Guild();
                    guild.Url = guildUrl;
                }

                guild.Name = nameOutcome.Value;
                
                _db.Guilds.AddOrUpdate(guild);
                _db.SaveChanges();

                return Outcomes.Success<Guild>()
                               .WithValue(guild);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Guild>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<string> ParseGuildName(HtmlDocument doc)
        {
            try
            {
                var guildHeader = doc.DocumentNode
                                 .Descendants("h1")
                                 .FirstOrDefault();

                if (guildHeader.IsNull())
                    return Outcomes.Failure<string>()
                                   .WithMessage("Unable to find the guild header. Swgoh.gg may have changed their layout.");

                var guildName = guildHeader.InnerText.Replace("\n", "");

                return Outcomes.Success<string>()
                               .WithValue(guildName);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }
    }
}
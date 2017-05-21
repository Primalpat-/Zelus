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

                var syncOutcome = CreateNewSynchronization(guild);
                if (syncOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                   .WithMessagesFrom(syncOutcome);

                var playerOutcome = PlayerSynchronizer.Execute(guild.Id, doc);
                if (playerOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                   .WithMessagesFrom(playerOutcome);

                return Outcomes.Success<Guild>()
                               .WithValue(guild);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Guild>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<GuildSynchronization> CreateNewSynchronization(Guild guild)
        {
            try
            {
                var sync = new GuildSynchronization
                {
                    GuildId = guild.Id,
                    Timestamp = DateTime.UtcNow
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

        #region "Guild creation and updating"

        public static IOutcome<Guild> CreateOrUpdateGuild(HtmlDocument playerCollection)
        {
            try
            {
                _db = new ZelusContext();
                var nameOutcome = ParseGuildNameFromCollection(playerCollection);
                if (nameOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                   .WithMessagesFrom(nameOutcome);

                var urlOutcome = ParseGuildUrl(playerCollection);
                if (urlOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                   .WithMessagesFrom(urlOutcome);

                var guild = _db.Guilds.FirstOrDefault(g => g.Url.ToLower() == urlOutcome.Value.ToLower());

                return CreateOrUpdateGuild(guild, nameOutcome.Value, urlOutcome.Value);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Guild>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<Guild> CreateOrUpdateGuild(Guild guild, string guildName, string guildUrl)
        {
            try
            {
                if (guild.IsNull())
                {
                    guild = new Guild();
                    guild.Url = guildUrl;
                }

                guild.Name = guildName;

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

        private static IOutcome<Guild> CreateOrUpdateGuild(HtmlDocument doc, Guild guild, string guildUrl)
        {
            try
            {
                var nameOutcome = ParseGuildName(doc);
                if (nameOutcome.Failure)
                    return Outcomes.Failure<Guild>()
                                   .WithMessagesFrom(nameOutcome);

                return CreateOrUpdateGuild(guild, nameOutcome.Value, guildUrl);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Guild>()
                               .WithMessage(ex.Message);
            }
        }

        #endregion

        #region "Parsing"

        private static IOutcome<string> ParseGuildName(HtmlDocument guildHomepage)
        {
            try
            {
                var name = guildHomepage.DocumentNode
                                        .Descendants("h1")
                                        .First()
                                        .InnerText
                                        .Replace("\n", "");

                return Outcomes.Success<string>()
                               .WithValue(name);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<string> ParseGuildNameFromCollection(HtmlDocument playerCollection)
        {
            try
            {
                var name = playerCollection.DocumentNode
                              .Descendants("p")
                              .First(p => p.InnerText.IsNotNullOrEmpty() && 
                                          p.InnerText.Contains("Guild"))
                              .LastChild
                              .FirstChild
                              .InnerText;

                return Outcomes.Success<string>()
                               .WithValue(name);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }

        private static IOutcome<string> ParseGuildUrl(HtmlDocument playerCollection)
        {
            try
            {
                var url = playerCollection.DocumentNode
                             .Descendants("p")
                             .First(p => p.InnerText.IsNotNull() && p.InnerText.Contains("Guild"))
                             .LastChild
                             .FirstChild
                             .Attributes["href"].Value;

                return Outcomes.Success<string>()
                               .WithValue($"https://swgoh.gg{url}");
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<string>()
                               .WithMessage(ex.Message);
            }
        }

        #endregion
    }
}
using HtmlAgilityPack;
using Zelus.Data.Models;

namespace Zelus.Web.Models.Synchronization.Models
{
    public class PlayerSynchronizationResult
    {
        public Player Player { get; set; }
        public HtmlDocument Document { get; set; }
    }
}
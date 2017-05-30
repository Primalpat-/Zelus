using System.Web.Mvc;
using Serilog;
using Zelus.Data.Models;

namespace Zelus.Web.Controllers
{
    public class ZelusControllerBase : Controller
    {
        protected readonly ILogger Log;
        protected readonly ZelusContext Db;

        public ZelusControllerBase()
        {
            Db = new ZelusContext();
            Log = new LoggerConfiguration().ReadFrom.AppSettings()
                                           .CreateLogger();
        }
    }
}
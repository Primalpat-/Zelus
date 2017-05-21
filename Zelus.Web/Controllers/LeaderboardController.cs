using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zelus.Web.Models.Factories;

namespace Zelus.Web.Controllers
{
    public class LeaderboardController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = LeaderboardModelFactory.GetModel();
            return View(model);
        }
    }
}
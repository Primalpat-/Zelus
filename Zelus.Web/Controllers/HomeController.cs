using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ether.Outcomes;
using LinqKit;
using Z.Core.Extensions;
using Zelus.Data;
using Zelus.Web.Helpers.Extensions;

namespace Zelus.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Home.");
        }
    }
}
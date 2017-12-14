﻿using System;
using System.Text;
using System.Web.Mvc;

namespace Zelus.Web.Areas.Api.Controllers.Shared
{
    public class ApiControllerBase : Controller
    {
        public JsonResult BigJson(object data)
        {
            return Json(data, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}
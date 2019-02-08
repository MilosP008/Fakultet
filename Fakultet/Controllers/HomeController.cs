﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fakultet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "O nama stranica.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt stranica.";

            return View();
        }
    }
}
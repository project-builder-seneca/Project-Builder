﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Builder_Development.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Manager m = new Manager();
            return View(m.GetAllCategories());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
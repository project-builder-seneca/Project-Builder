using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Builder_Development.Controllers
{
    public class HomeController : Controller
    {
        Manager m = new Manager();

        public ActionResult Index()
        {
            m.LoadData();
            dynamic obj = new ExpandoObject();
            obj.Categories = m.GetAllCategories();
            obj.Ideas = m.GetAllIdeas();

            return View(obj);
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
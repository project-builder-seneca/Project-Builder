using Project_Builder_Development.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Builder_Development.Controllers
{
    public class RequestController : Controller
    {
        Manager m = new Manager();
        // GET: Request
        [Authorize]
        public ActionResult Index()
        {
            var obj = m.showRequest();
            List<RequestBaseViewModel> o = new List<RequestBaseViewModel>();
            for (int i = 0; i < obj.Count(); i++) {
                if (obj.ElementAt(i).UserName == HttpContext.User.Identity.Name) {
                    o.Add(obj.ElementAt(i));
                }
            }
            return View(o);
        }

        // GET: Request/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View(m.showOneRequest(id));
        }

        [Authorize]
        public ActionResult RequestsOnIdea() {

            var obj = m.showRequest();

            List<RequestBaseViewModel> o = new List<RequestBaseViewModel>();
            for (int i = 0; i < obj.Count(); i++)
            {
                if (obj.ElementAt(i).Ideas.Owner == HttpContext.User.Identity.Name)
                {
                    o.Add(obj.ElementAt(i));
                }
            }
            return View(o);
        }

        [Authorize]
        public ActionResult Details1(int id)
        {
            return View(m.showOneRequest(id));
        }

        [Authorize]
        public ActionResult AddMember(int id) {

            var obj = m.showOneRequest(id);
            var IdeaId = obj.IdeaId;

            var user = new UserName();
            user.Name = obj.UserName;

            var addedUser = m.AddorFindUser(user);

            m.AddUserIdea(obj, addedUser);

            return RedirectToAction("../Idea/IdeaDetails",new { id = IdeaId });
        }

        // GET: Request/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Request/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Request/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Request/Delete/5
        public ActionResult Delete(int id)
        {
            var obj = m.showOneRequest(id);

            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            else {
                return View(obj);
            }
        }

        // POST: Request/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var delete = m.deleteRequest(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

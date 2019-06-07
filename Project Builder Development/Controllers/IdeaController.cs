using Project_Builder_Development.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Builder_Development.Controllers
{
    public class IdeaController : Controller
    {
        Manager m = new Manager();
        // GET: Idea
        
        public ActionResult Ideas()
        {
            return View(m.GetAllIdeas());
        }

        // GET: Idea/Details/5
        public ActionResult IdeaDetails(int? id)
        {
            return View(m.GetOneIdea(id));
        }

        // GET: Idea/Create
        public ActionResult AddIdea()
        {
            var obj = new IdeaFormViewModel();

            obj.SkillList = new MultiSelectList(m.GetAllSkills(), dataValueField: "SkillId", dataTextField: "Name");
            obj.CategoryList = new SelectList(m.GetAllCategories(), dataValueField: "CategoryId", dataTextField: "Name");

            obj.Owner = HttpContext.User.Identity.Name;

            return View(obj);
        }

        // POST: Idea/Create
        [HttpPost]
        public ActionResult AddIdea(IdeaAddViewModel newIdea)
        {
            newIdea.Owner = HttpContext.User.Identity.Name;

            var addedIdea = m.AddIdea(newIdea);

            if (addedIdea == null)
            {
                return View(newIdea);
            }
            else {
                return RedirectToAction("../Idea/IdeaDetails", new { id = addedIdea.IdeaId });
            }
        }

        // GET: Idea/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Idea/Edit/5
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

        // GET: Idea/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Idea/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

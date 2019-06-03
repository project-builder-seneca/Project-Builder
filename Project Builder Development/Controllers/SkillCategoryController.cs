using Project_Builder_Development.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Builder_Development.Controllers
{
    public class SkillCategoryController : Controller
    {

        Manager m = new Manager();
        // GET: SkillCategory
        [AllowAnonymous]
        public ActionResult Skill()
        {
            return View(m.GetAllSkills());
        }

        [AllowAnonymous]
        public ActionResult Category()
        {
            return View(m.GettAllCategories());
        }

        // GET: SkillCategory/Details/5
        [AllowAnonymous]
        public ActionResult SkillsDetails(int? id)
        {
            return View(m.GetOneSkill(id));
        }

        [AllowAnonymous]
        public ActionResult CategoriesDetails(int? id)
        {
            return View(m.GetOneCategory(id));
        }

        // GET: SkillCategory/Create
        [AllowAnonymous]
        public ActionResult AddSkill()
        {
            return View();
        }

        // POST: SkillCategory/Create
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddSkill(SkillAddViewModel newSkill)
        {
            try
            {
                if (!ModelState.IsValid) {
                    return View(newSkill);
                }

                var addedSkill = m.AddSkill(newSkill);

                if (addedSkill == null)
                {
                    return View(newSkill);
                }
                else
                {
                    return RedirectToAction("../SkillCategory/SkillsDetails", new { id = addedSkill.SkillId});
                }
            }
            catch
            {
                return View(newSkill);
            }
        }

        // GET: SkillCategory/Create
        [AllowAnonymous]
        public ActionResult AddCategory()
        {
            return View();
        }

        // POST: SkillCategory/Create
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddCategory(CategoryAddViewModel newCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newCategory);
                }

                var addedCategory = m.AddCategory(newCategory);

                if (addedCategory == null)
                {
                    return View(newCategory);
                }
                else
                {
                    return RedirectToAction("../SkillCategory/Categories/Details", new { id = addedCategory.CategoryId});
                }
            }
            catch
            {
                return View(newCategory);
            }
        }

        // GET: SkillCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SkillCategory/Edit/5
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

        // GET: SkillCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SkillCategory/Delete/5
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

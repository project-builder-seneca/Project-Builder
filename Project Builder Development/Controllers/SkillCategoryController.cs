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
        public int skillId = 1;
        public int categoryId = 1;
        Manager m = new Manager();
        // GET: SkillCategory

        [Authorize(Roles = "Admin")]
        public ActionResult Skill()
        {
            return View(m.GetAllSkills());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Category()
        {
            return View(m.GetAllCategories());
        }

        // GET: SkillCategory/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult SkillsDetails(int? id)
        {
            return View(m.GetOneSkill(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CategoriesDetails(int? id)
        {
            return View(m.GetOneCategory(id));
        }

        // GET: SkillCategory/Create
        [Authorize(Roles = "Admin")]
        public ActionResult AddSkill()
        {
            var obj = new SkillAddViewModel();
            obj.SkillId = skillId++;
            return View(obj);
        }

        // POST: SkillCategory/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult AddCategory()
        {
            var obj = new CategoryAddViewModel();
            obj.CategoryId = categoryId++;
            return View(obj);
        }

        // POST: SkillCategory/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    return RedirectToAction("../SkillCategory/CategoriesDetails", new { id = addedCategory.CategoryId});
                }
            }
            catch
            {
                return View(newCategory);
            }
        }

        // GET: SkillCategory/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SkillCategory/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(m.GetOneSkill(id));
        }

        // POST: SkillCategory/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                m.deleteSkill(id);

                return RedirectToAction("Skill");
            }
            catch
            {
                return View();
            }
        }
    }
}

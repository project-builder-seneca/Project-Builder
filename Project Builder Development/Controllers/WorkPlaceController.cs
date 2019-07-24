using Project_Builder_Development.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Project_Builder_Development.Controllers
{
    public class WorkPlaceController : Controller
    {
        Manager m = new Manager();
        // GET: WorkPlace
        public ActionResult Index(int id)
        { 
            return View(m.GetOneIdea(id));
        }

        public ActionResult Team(int id) {
            return View(m.GetOneIdea(id));
        }

        public ActionResult Tasks(int id) {
            return View(m.GetOneIdea(id));
        }

        public ActionResult CreateTask(int id) {

            bool check = true;
            // Getting all the members
            var Idea = m.GetOneIdea(id);
            var username = new List<UserName>();
            foreach (var item in Idea.Users) {
                username.Add(item);
            }

            var owner = new UserName();
            owner.Name = Idea.Owner;
            owner = m.AddorFindUser(owner);

            username.Add(owner);


            var obj = new TaskGivenFormViewModel();
            obj.SkillList = new MultiSelectList(m.GetAllSkills(), dataValueField: "SkillId", dataTextField: "Name");
            obj.UserNameList = new MultiSelectList(username, dataValueField: "UserId", dataTextField: "Name");
            obj.IdeaId = id;

            return View(obj);
        }

        [HttpPost]
        public ActionResult CreateTask(TaskGivenAddViewModel newTask){
                var addedTask = m.AddTask(newTask);

                if (addedTask != null)
                {
                    return RedirectToAction("Tasks",new {id = addedTask.IdeaId });
                }
                else {
                    return View();
                }
        }
    }
}

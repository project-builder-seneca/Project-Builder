using Project_Builder_Development.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Builder_Development.Controllers
{
    public class IdeaController : Controller
    {
        public int IdeaId = 1;
        public int ReplyId = 1;
        public int ReplyReplyId = 1;
        public int RequestId = 1;
        public int UserId = 1;

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
        [Authorize]
        public ActionResult AddIdea()
        {
            var obj = new IdeaFormViewModel();

            obj.SkillList = new MultiSelectList(m.GetAllSkills(), dataValueField: "SkillId", dataTextField: "Name");
            obj.CategoryList = new SelectList(m.GetAllCategories(), dataValueField: "CategoryId", dataTextField: "Name");

            obj.Owner = HttpContext.User.Identity.Name;
            obj.IdeaId = IdeaId++;

            return View(obj);
        }

        // POST: Idea/Create
        [HttpPost]
        [Authorize]
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


        // Reply 

        [Route("Idea/{id}/addreply")]
        [Authorize]
        public ActionResult AddReply(int Id)
        {
            var obj = new ReplyBaseViewModel();
            obj.UserName = HttpContext.User.Identity.Name;
            obj.IdeaId = Id;
            return View(obj);
        }

        [Route("Idea/{id}/addreply")]
        [HttpPost]
        [Authorize]
        public ActionResult AddReply(ReplyBaseViewModel newReply)
        {
            newReply.ReplyId = ReplyId;
            ReplyId++;
            var addedReply = m.AddReply(newReply);
            if (addedReply == null)
            {
                return View(newReply);
            }
            else
            {
                return RedirectToAction("../Idea/IdeaDetails", new { id = addedReply.IdeaId });
            }
        }

        [Route("Idea/{id}/reply")]
        [Authorize]
        public ActionResult AddReplyReply(int id1, int id2)
        {
            var obj = new ReplyReplyBaseViewModel();
            obj.ReplyIdd = id1;
            obj.IdeaId = id2;
            return View(obj);
        }

        [Route("Idea/{id}/reply")]
        [HttpPost]
        [Authorize]
        public ActionResult AddReplyReply(ReplyReplyBaseViewModel newReply)
        {
            newReply.ReplyId = ReplyReplyId;
            ReplyReplyId++;
            var addedReply = m.AddReplyReply(newReply);
            if (addedReply == null)
            {
                return View(newReply);
            }
            else
            {
                return RedirectToAction("../Idea/IdeaDetails", new { id = addedReply.IdeaId });
            }
        }

        [Route("Idea/{id}/AddRequest")]
        [Authorize]
        public ActionResult AddRequest(int Id)
        {
            var obj = new RequestBaseViewModel();

            obj.UserName = HttpContext.User.Identity.Name;
            obj.RequestId = RequestId++;
            var idea = m.GetOneIdea(Id);
            obj.Ideas = idea;
            obj.IdeaId = Id;
            obj.Patner = false;
            obj.Volunteer = false;
            obj.Investor = false;

            return View(obj);
        }

        [Route("Idea/{id}/AddRequest")]
        [HttpPost]
        [Authorize]
        public ActionResult AddRequest(RequestBaseViewModel newRequest)
        {
            bool check = false;
            var req = m.showRequest();
            var index = 0;

            var addedRequest = new RequestBaseViewModel();

            foreach (var item in req) {
                if (item.UserName == newRequest.UserName && item.IdeaId == newRequest.IdeaId)
                {
                    ViewBag.ErrorMessage = "You have already sent a request to this Idea before!";
                    index = item.RequestId;
                    check = true;
                }
            }
            if(check == false) {
                addedRequest = m.AddRequest(newRequest);
            }

            if (addedRequest == null)
            {
                newRequest = m.showOneRequest(index);
                return View(newRequest);
            }
            else
            {
                if (check == true) {
                    newRequest = m.showOneRequest(index);
                    return View(newRequest);
                } else { 
                return RedirectToAction("../Idea/IdeaDetails", new { id = newRequest.IdeaId });
                }
            }
        }



        [Authorize]
        public ActionResult Like(int ID) {

            var obj = new React();
            obj.user = HttpContext.User.Identity.Name;

            m.Like(ID, obj);

            return RedirectToAction("../Idea/IdeaDetails", new { id = ID });
        }

        [Authorize]
        public ActionResult DisLike(int ID)
        {
            var obj = new React();
            obj.user = HttpContext.User.Identity.Name;

            m.DisLike(ID, obj);

            return RedirectToAction("../Idea/IdeaDetails", new { id = ID });
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

        //Get: /Idea/Project
        public ActionResult Project()
        {
            // Create a view model object
            var accountDetails = new AccountDetails();

            // Identity object "name" (i.e. not the claim)
            accountDetails.UserName = User.Identity.Name;

            return View(m.GetAllProject(accountDetails.UserName));
        }
    }
}

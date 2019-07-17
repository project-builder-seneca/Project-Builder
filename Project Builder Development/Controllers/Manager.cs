using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Web;
using Project_Builder_Development.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project_Builder_Development.Controllers
{
    public class Manager
    {
        public int RoleId = 0;
        public int ReactId = 1;
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();
        private ApplicationDbContext ds1 = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Idea Maps

                // Category Maps

                cfg.CreateMap<Category,CategoryBaseViewModel>();
                cfg.CreateMap<CategoryAddViewModel, Category>();

                // Skill Maps
                cfg.CreateMap<Skill,SkillBaseViewModel>();
                cfg.CreateMap<SkillAddViewModel, Skill>();

                //Idea Maps
                cfg.CreateMap<Idea, IdeaBaseViewModel>();
                cfg.CreateMap<IdeaFormViewModel, IdeaAddViewModel>();
                cfg.CreateMap<IdeaAddViewModel, Idea>();

                //Register
                cfg.CreateMap<RegisterViewModel, RegisterFormViewModel>();

                //Reply
                cfg.CreateMap<Reply,ReplyBaseViewModel>();
                cfg.CreateMap<ReplyBaseViewModel,Reply>();

                //ReplyReply
                cfg.CreateMap<ReplyReply, ReplyReplyBaseViewModel>();
                cfg.CreateMap<ReplyReplyBaseViewModel,ReplyReply>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Idea Functions...

        public IEnumerable<IdeaBaseViewModel> GetAllIdeas()
        {
            return mapper.Map<IEnumerable<Idea>, IEnumerable<IdeaBaseViewModel>>(ds.Ideas);
        }

        public IdeaBaseViewModel GetOneIdea(int? id)
        {
            var obj = ds.Ideas.Include("VolSkills").Include("PatSkills").Include("Category").Include("Replies").Include("Replies.ReplyReplies").SingleOrDefault(i => id == i.IdeaId);
            return obj == null ? null : mapper.Map<Idea, IdeaBaseViewModel>(obj);
        }

        public IdeaBaseViewModel AddIdea(IdeaAddViewModel newIdea)
        {
            var category = ds.Categories.Find(newIdea.CategoryId);

            var addedIdea = ds.Ideas.Add(mapper.Map<IdeaAddViewModel,Idea>(newIdea));

            foreach (var id in newIdea.PatSkillIds) {
                var skill = ds.Skills.Find(id);
                addedIdea.PatSkills.Add(skill);
            }

            foreach (var id in newIdea.VolSkillIds) {
                var skill = ds.Skills.Find(id);
                addedIdea.VolSkills.Add(skill);
            }

            addedIdea.Category = category;

            ds.SaveChanges();


            return addedIdea == null ? null : mapper.Map<Idea, IdeaBaseViewModel>(addedIdea);
        }

        public void Like(int id, React obj) {

            var idea = ds1.Ideas.Include("Reacts").SingleOrDefault(e => id == e.IdeaId);
            obj.IdeasId = id;
            obj.like = false;
            obj.dislike = false;

            AddReact(obj);

            foreach (var item in idea.Reacts) {
                if(obj.user == item.user){
                    if (item.like == false) {
                        idea.Like += 1;
                        item.dislike = true;
                        item.like = true;
                    }
                }
            }
            ds1.SaveChanges();
        }

        public void DisLike(int id, React obj){

            var idea = ds1.Ideas.Include("Reacts").SingleOrDefault(e => id == e.IdeaId);
            obj.IdeasId = id;
            obj.like = false;
            obj.dislike = false;

            AddReact(obj);

            foreach (var item in idea.Reacts)
            {
                if (obj.user == item.user)
                {
                    if (item.dislike == false)
                    {
                        idea.Dislike += 1;
                        item.like = true;
                        item.dislike = true;
                    }
                }
            }

            ds1.SaveChanges();

        }

        // Add React

        public void AddReact(React obj) {

            var idea = ds1.Ideas.Find(obj.IdeasId);
            var check = ds.Reacts.SingleOrDefault(e => obj.user  == e.user && obj.IdeasId == e.IdeasId);

            if (check == null)
            {
                idea.Reacts.Add(obj);
            }

            ds1.SaveChanges();
            ds.SaveChanges();
        }

        // Category Functions

        public IEnumerable<CategoryBaseViewModel> GetAllCategories()
        {
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryBaseViewModel>>(ds.Categories);
        }

        public CategoryBaseViewModel GetOneCategory(int? id)
        {
            var obj = ds.Categories.SingleOrDefault(c => id == c.CategoryId);
            return mapper.Map<Category, CategoryBaseViewModel>(obj);
        }

        public CategoryBaseViewModel AddCategory(CategoryAddViewModel newCategory) {
            var addedCategory = ds.Categories.Add(mapper.Map<CategoryAddViewModel,Category>(newCategory));
            ds.SaveChanges();

            return addedCategory == null ? null : mapper.Map<Category,CategoryBaseViewModel>(addedCategory);
        }

        // Skill Functions

        public IEnumerable<SkillBaseViewModel> GetAllSkills()
        {
            return mapper.Map<IEnumerable<Skill>, IEnumerable<SkillBaseViewModel>>(ds.Skills);
        }

        public SkillBaseViewModel GetOneSkill(int? id)
        {
            var obj = ds.Skills.SingleOrDefault(s => id == s.SkillId);
            return mapper.Map<Skill, SkillBaseViewModel>(obj);
        }

        public SkillBaseViewModel AddSkill(SkillAddViewModel newSkill) {
            var addedSkill = ds.Skills.Add(mapper.Map<SkillAddViewModel,Skill>(newSkill));
            ds.SaveChanges();

            return addedSkill == null ? null : mapper.Map<Skill, SkillBaseViewModel>(addedSkill);
        }

        // Get RoleClaims to string

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Reply

        /*public IEnumerable<ReplyBaseViewModel> GetAllReplies()
        {
            return mapper.Map<IEnumerable<Reply>, IEnumerable<ReplyBaseViewModel>>(ds.Replies);
        }

        public ReplyBaseViewModel GetOneReply(int? i)
        {
            var obj = ds.Replies.SingleOrDefault(e => i == e.ReplyId);
            return mapper.Map<Reply, ReplyBaseViewModel>(obj);
        }*/

        public ReplyBaseViewModel AddReply(ReplyBaseViewModel newReply)
        {
            var addedReply = ds.Replies.Add(mapper.Map<ReplyBaseViewModel, Reply>(newReply));

            var idea = ds.Ideas.Find(newReply.IdeaId);
            idea.Replies.Add(addedReply);

            ds.SaveChanges();
            return addedReply == null ? null : mapper.Map<Reply, ReplyBaseViewModel>(addedReply);
        }

        public ReplyReplyBaseViewModel AddReplyReply(ReplyReplyBaseViewModel newReplyReply) {
            var addedReplyReply = ds.ReplyReplies.Add(mapper.Map<ReplyReplyBaseViewModel,ReplyReply>(newReplyReply));

            var reply = ds.Replies.Find(newReplyReply.ReplyIdd);
            reply.ReplyReplies.Add(mapper.Map<ReplyReplyBaseViewModel,ReplyReply>(newReplyReply));

            ds.SaveChanges();
            return addedReplyReply == null ? null : mapper.Map<ReplyReply,ReplyReplyBaseViewModel>(addedReplyReply);
        }

        public void LoadData() {

            // Roles Table;

            if(ds.RoleClaims.Count() == 0) { 
                var admin = new RoleClaim();
                admin.Id = RoleId++;
                admin.Name = "Admin";

                var user = new RoleClaim();
                user.Id = RoleId++;
                user.Name = "User";

                ds.RoleClaims.Add(mapper.Map<RoleClaim>(admin));
                ds.RoleClaims.Add(mapper.Map<RoleClaim>(user));

                ds.SaveChanges();
            }
            // Roles added - END of Roles 
        }
    }
}
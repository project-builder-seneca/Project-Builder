using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Builder_Development.Models
{
    public class Idea {

        public Idea() {
            PatSkills = new List<Skill>();
            VolSkills = new List<Skill>();
            Replies = new List<Reply>();
            PatUserNames = new List<UserName>();
            VolUserNames = new List<UserName>();
            InvestUserNames = new List<UserName>();
        }

        [Required]
        [Display(Name = "Idea ID")]
        public int IdeaId { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public String Owner { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "Title cannot exceed more than {0} words", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Description cannot exceed more than {0} words", MinimumLength = 5)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Investment Goal")]
        public int InvestmentGoal { get; set; }

        [Display(Name = "Partners Required: ")]
        public int PartnersRequired { get; set; }

        [Display(Name = "Skills Required for Partners: ")]
        public ICollection<Skill> PatSkills { get; set; }

        [Display(Name = "Volunteers Required: ")]
        public int VolunteersRequired { get; set; }

        [Display(Name = "Skills required for Volunteers: ")]
        public ICollection<Skill> VolSkills { get; set; }

        [Display(Name = "Reply")]
        public ICollection<Reply> Replies { get; set; }

        [Display(Name = "Like: ")]
        public int Like { get; set; }

        [Display(Name = "Dislike: ")]
        public int Dislike { get; set; }

        public ICollection<React> Reacts { get; set; }

        public ICollection<UserName> PatUserNames { get; set; }

        public ICollection<UserName> VolUserNames { get; set; }

        public ICollection<UserName> InvestUserNames { get; set; }
    }

    public class Category
    {

        [Required]
        [Display(Name = "Category Id: ")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name: ")]
        public String Name { get; set; }

    }

    public class Skill
    {

        [Required]
        [Display(Name = "Skill Id: ")]
        public int SkillId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "Skill Name: ")]
        public string Name { get; set; }

    }

    public class RoleClaim {

        [Required]
        [Display(Name = "Role Id: ")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Role: ")]
        public string Name { get; set; }

    }

    public class Reply
    {
        public Reply() {
            ReplyReplies = new List<ReplyReply>();
        }

        [Required]
        [Display(Name = "Id: ")]
        public int ReplyId { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string message { get; set; }

        [Required]
        [Display(Name = "By: ")]
        public string UserName { get; set; }

        [Required]
        public int IdeaId { get; set; }

        [Display(Name = "Reply")]
        public ICollection<ReplyReply> ReplyReplies { get; set; }
    }

    public class ReplyReply
    {

        [Key]
        [Required]
        [Display(Name = "Id: ")]
        public int ReplyId { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string message { get; set; }

        [Required]
        public int ReplyIdd { get; set; }

        [Required]
        public int IdeaId { get; set; }
    }

    public class React
    {

        [Key]
        [Required]
        [Display(Name = "Id")]
        public int ReactId { get; set; }

        [Required]
        [Display(Name = "User: ")]
        public string user { get; set; }

        public bool like { get; set; }

        public bool dislike { get; set; }

        public int IdeasId { get; set; }
    }

    public class UserName{
        [Key]
        [Required]
        public int UserId { get; set; } 
        
        [Required]
        public string Name { get; set; }
    }

    public class Request {
        [Required]
        [Key]
        public int RequestId { get; set; }

        public string Message { get; set; }

        [Required]
        public Idea Ideas { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int IdeaId { get; set; }

        public bool Patner { get; set; }

        public bool Volunteer { get; set; }

        public bool Investor { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Builder_Development.Models
{
    public class IdeaBaseViewModel
    {

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

        [Display(Name = "Partners Required")]
        public int PartnersRequired { get; set; }

        public IEnumerable<SkillBaseViewModel> Skills { get; set; }

        [Display(Name = "Volunteers Required")]
        public int VolunteersRequired { get; set; }

        [Display(Name = "Reply")]
        public IEnumerable<ReplyBaseViewModel> Replies { get; set; }

        [Display(Name = "Like: ")]
        public int Like { get; set; }

        [Display(Name = "Dislike: ")]
        public int Dislike { get; set; }

        public IEnumerable<UserName> Users { get; set; }

        public IEnumerable<TaskGivenBaseViewModel> Tasks { get; set; }
    }

    public class IdeaFormViewModel : IdeaBaseViewModel{

        public MultiSelectList SkillList { get; set; }
        public SelectList CategoryList { get; set; }
    }

    public class IdeaAddViewModel : IdeaBaseViewModel {

        public IEnumerable<int> PatSkillIds { get; set; }
        public IEnumerable<int> VolSkillIds { get; set; }
        public int CategoryId { get; set; }
    }

    public class RequestBaseViewModel
    {
        [Required]
        [Key]
        public int RequestId { get; set; }

        public string Message { get; set; }

        [Required]
        public IdeaBaseViewModel Ideas { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int IdeaId { get; set; }

        public bool Patner { get; set; }

        public bool Volunteer { get; set; }

        public bool Investor { get; set; }
    }

    public class TaskGivenBaseViewModel
    { 
        [Required]
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }

        public IEnumerable<UserName> AssignedTo { get; set; }

        public DateTime TargetDate { get; set; }

        public IEnumerable<SkillBaseViewModel> Skills { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int IdeaId { get; set; }
    }

    public class TaskGivenFormViewModel : TaskGivenBaseViewModel
    {
        public MultiSelectList UserNameList { get; set; }
        public MultiSelectList SkillList { get; set; }
    }

    public class TaskGivenAddViewModel : TaskGivenBaseViewModel
    {
        public IEnumerable<int> UserNameIds { get; set; }
        public IEnumerable<int> SkillIds { get; set; }
    }
}
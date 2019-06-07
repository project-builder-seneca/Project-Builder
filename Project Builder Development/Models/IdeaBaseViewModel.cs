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

        [Display(Name = "Partners Required: ")]
        public int PartnersRequired { get; set; }

        [Display(Name = "Skills Required for Partners: ")]
        [StringLength(25, ErrorMessage = "Please Choose from the provided list!", MinimumLength = 2)]
        public IEnumerable<SkillBaseViewModel> PatSkills { get; set; }

        [Display(Name = "Volunteers Required: ")]
        public int VolunteersRequired { get; set; }

        [Display(Name = "Skills required for Volunteers: ")]
        public IEnumerable<SkillBaseViewModel> VolSkills { get; set; }
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
}
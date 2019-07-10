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


    public class Task
    {

        [Key]
        public int taskNo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Task Name: ")]
        public string taskName { get; set; }

        [Required]
        [Display(Name = "Start Date: ")]
        public DateTime startDate { get; set; }

        [Required]
        [Display(Name = "Due Date: ")]
        public DateTime dueDate { get; set; }

        [Required]
        [Display(Name = "End Date: ")]
        public DateTime endDate { get; set; }
    }

    public class PersonalSkill
    {
        [Key]
        public int pSkillNo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Personal skill name: ")]
        public string pSkillName { get; set; }

    }

    public class Expense
    {
        [Key]
        public int expenseNo { get; set; }

        [Required]
        [Display(Name = "Cost: ")]
        public int cost { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Coment: ")]
        public string coment { get; set; }

        [Required]
        [Display(Name = "Date: ")]
        public DateTime expenseDate { get; set; }

    }

}
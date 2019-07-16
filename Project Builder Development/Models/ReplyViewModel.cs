using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Builder_Development.Models
{
    public class ReplyBaseViewModel
    {
        [Required]
        [Display(Name = "Id: ")]
        public int ReplyId { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string message { get; set; }

        [Required]
        [Display(Name = "By: ")]
        public string UserName { get; set; }

        [Display(Name = "IdeaId")]
        public int IdeaId { get; set; }

        [Display(Name = "Reply")]
        public IEnumerable<ReplyReplyBaseViewModel> ReplyReplies { get; set; }
    }

    public class ReplyReplyBaseViewModel {
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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project_Builder_Development.Models
{
    public class ResetPasswordModel
    {
        [StringLength(25, ErrorMessage = "Password must be {2} to {0} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "New Password required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}
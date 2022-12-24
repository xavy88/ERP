using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.Models.VM
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage="The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string RoleSelected { get; set; }

    }
}

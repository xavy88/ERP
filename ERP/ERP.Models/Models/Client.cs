using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Business Name")]
        public string BusinessName { get; set; }
        public string Url { get; set; }
        [Required]
        [Display(Name = "Business Phone")]
        public string BusinessPhone { get; set; }
        [Required]
        [Display(Name = "Business Email")]
        public string BusinessEmail { get; set; }
        [Display(Name = "Business Address")]
        public string BusinessAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Industry { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Client Phone")]
        public string ClientPhone { get; set; }
        [Display(Name = "Client Email")]
        public string ClientEmail { get; set; }
        public string Access { get; set; }
        public string Notes { get; set; }
        [ValidateNever]
        public string Logo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool Active { get; set; } = true;

    }
}

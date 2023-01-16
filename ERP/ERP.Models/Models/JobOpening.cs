using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models
{
    public class JobOpening
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Job")]
        public string JobTitle { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public Department Department { get; set; }
        [Display(Name ="Job Type")]
        public string JobType { get; set; }
        public string Benefit { get; set; }
        [Required]
        public int JobPositionId { get; set; }
        [ForeignKey("JobPositionId")]
        [ValidateNever]
        public JobPosition JobPosition { get; set; }
        public string Skills { get; set; }
        public string Education { get; set; }
        public string Salary { get; set; }
        [Display(Name ="Shift & Schedule")]
        public string Shift { get; set; }
        [Display(Name = "Minimum Experience")]
        public string MinimumExperience { get; set; }
        public int Vacancy { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Contact Email")]
        public string Email { get; set; }
        public DateTime Expire { get; set; }
        public string Language { get; set; }
        [Display(Name = "Job Description ")]
        public string JobDescription { get; set; }
        public string HiringLead { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool Active { get; set; } = true;
    }
}

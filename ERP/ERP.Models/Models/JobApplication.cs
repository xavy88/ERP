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
    public class JobApplication
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        [ValidateNever]
        public string CV { get; set; }
        [Required]
        public int JobOpeningId { get; set; }
        [ForeignKey("JobOpeningId")]
        [ValidateNever]
        public JobOpening JobOpening { get; set; }
        public string EvaluatedBy { get; set; }
        public DateTime EvaluatedDateTime { get; set; }
        public bool Evaluated { get; set; } = false;

    }
}

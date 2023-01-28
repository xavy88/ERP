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
    public class PayRollOrder
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        [Required]
        public int Sequence { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public DateTime PayentDate { get; set; }
        public bool Paid { get; set; } = false;
        public bool Closed { get; set; } = false;
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
     }
}

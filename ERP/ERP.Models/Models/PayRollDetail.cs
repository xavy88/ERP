using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models
{
    public class PayRollDetail
    {
        public int Id { get; set; }
        [Required]
        public int PayRollOrderId { get; set; }
        [ForeignKey("PayRollOrderId")]
        [ValidateNever]
        public PayRollOrder PayRollOrder { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employee Employee { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Required]
        [Precision(18, 2)]
        [Display(Name ="Extra Payment")]
        public decimal ExtraPayment { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Deduction { get; set; }
        public string Remark { get; set; }
        public bool Paid { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
     }
}

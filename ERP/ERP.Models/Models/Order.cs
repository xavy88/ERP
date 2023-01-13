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
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        [ValidateNever]
        public Service Service{ get; set; }
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        [ValidateNever]
        public Client Client { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employee Employee { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime BillingDate { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool Paid { get; set; } = false;
        public bool Closed { get; set; } = false;
    }
}

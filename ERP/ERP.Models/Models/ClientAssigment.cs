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
    public class ClientAssigment
    {
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        [ValidateNever]
        public Client Client { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public Department Department{ get; set; }
        [Required]
        public int JobPositionId { get; set; }
        [ForeignKey("JobPositionId")]
        [ValidateNever]
        public JobPosition JobPosition { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employee Employee { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}

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
    public class TaskAssigment
    {
        public int Id { get; set; }
        [Required]
        public int TasksId { get; set; }
        [ForeignKey("TasksId")]
        [ValidateNever]
        public Tasks Tasks{ get; set; }
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
        public DateTime DueDate { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string ClosedBy { get; set; }
        public DateTime ClosedDateTime { get; set; }
        public bool Closed { get; set; } = false;
    }
}

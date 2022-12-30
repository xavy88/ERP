using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string SSN { get; set; }
        [Required]
        [Display(Name = "Identification #")]
        public string IdentificationId { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Work Email")]
        public string WorkEmail { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Personal Email")]
        public string PersonalEmail { get; set; }
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        public string Relationship { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Address Contact")]
        public string AddressContact { get; set; }
        [Display(Name = "Pay Amount")]
        public double PayAmount { get; set; }
        [Display(Name="Report To")]
        public string ReportTo { get; set; }
        [Display(Name="Photo")]
        public string ImageUrl { get; set; }
        [Required]
        public int JobPosistionId { get; set; }
        [ForeignKey("JobPosistionId")]
        public JobPosition jobPosition { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool Active { get; set; } = true;
    }
}

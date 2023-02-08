using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models.VM
{
    public class ClientAssigmentViewModel
    {

        public ClientAssigment ClientAssigment {get;set;}
        [ValidateNever]
        public IEnumerable<SelectListItem> ClientList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> JobPositionList { get; set; }
    }
}

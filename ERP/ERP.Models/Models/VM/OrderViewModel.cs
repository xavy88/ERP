using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models.VM
{
    public class OrderViewModel
    {

        public Order Order {get;set;}
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ClientList { get; set; }
    }
}

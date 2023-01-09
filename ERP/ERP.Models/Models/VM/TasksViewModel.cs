using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models.VM
{
    public class TasksViewModel
    {

        public Tasks Tasks {get;set;}
        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmenList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> JobPositionList { get; set; }
    }
}

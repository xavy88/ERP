using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models.Models.VM
{
    public class JobApplicationViewModel
    {

        public JobApplication JobApplication {get;set;}
        [ValidateNever]
        public IEnumerable<SelectListItem> JobOpeningList { get; set; }
    }
}

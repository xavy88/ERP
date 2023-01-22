using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ERP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<JobOpening> jobOpeningList = _unitOfWork.JobOpening.GetAll(jo => jo.Active == true, includeProperties: "Department,JobPosition");
            return View(jobOpeningList);
        }
        
        public IActionResult JobDetail(int? id)
        {
            JobOpeningViewModel jobOpeningVM = new()
            {
                JobOpening = new(),
                DepartmenList = _unitOfWork.Department.GetAll(d => d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                JobPositionList = _unitOfWork.JobPosition.GetAll(jp => jp.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
            };

            if (id == null || id == 0)
            {
                //Create
                return View(jobOpeningVM);
            }
            else
            {
                //Update
                jobOpeningVM.JobOpening = _unitOfWork.JobOpening.GetFirstOrDefault(e => e.Id == id);
                return View(jobOpeningVM);
            }
            return View(jobOpeningVM);
        }
        

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
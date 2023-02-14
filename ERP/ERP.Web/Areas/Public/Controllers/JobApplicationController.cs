using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.Public.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        public JobApplicationController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<JobApplication> objJobApplicationList = _unitOfWork.JobApplication.GetAll(jo => jo.Evaluated == false, includeProperties: "JobOpening");
            return View(objJobApplicationList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            JobApplicationViewModel jobApplicationVM = new()
            {
                JobApplication = new(),
                JobOpeningList = _unitOfWork.JobOpening.GetAll(d => d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.JobTitle,
                    Value = e.Id.ToString(),
                }),
            };

            if (id == null || id == 0)
            {
                //Create
                return View(jobApplicationVM);
            }
            else
            {
                //Update
                jobApplicationVM.JobApplication = _unitOfWork.JobApplication.GetFirstOrDefault(e => e.Id == id);
                return View(jobApplicationVM);
            }
            return View(jobApplicationVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(JobApplicationViewModel obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\cv");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.JobApplication.CV != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.JobApplication.CV.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.JobApplication.CV = @"\img\cv\" + fileName + extension;
                }

                if (obj.JobApplication.Id == 0)
                {
                    _unitOfWork.JobApplication.Add(obj.JobApplication);
                    TempData["success"] = "Your application was submitted";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj.JobApplication);
        }

        public IActionResult Evaluated(int? id)
        {
            JobApplication jobApplication = _unitOfWork.JobApplication.GetFirstOrDefault(jo => jo.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
            string str = claim.ToString();
            string ext = str.Remove(0, 60);

            jobApplication.EvaluatedBy = ext;
            jobApplication.EvaluatedDateTime = DateTime.Now;

            _unitOfWork.JobApplication.Evaluated(jobApplication);
            _unitOfWork.Save();
            TempData["success"] = "Candidate evaluation successfully";
            return RedirectToAction("Index");

        }

        public IActionResult Details(int? id)
        {
            JobApplicationViewModel jobApplicationVM = new()
            {
                JobApplication = new(),
                JobOpeningList = _unitOfWork.JobOpening.GetAll(d => d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.JobTitle,
                    Value = e.Id.ToString(),
                }),
            };

                jobApplicationVM.JobApplication = _unitOfWork.JobApplication.GetFirstOrDefault(e => e.Id == id);
                return View(jobApplicationVM);
            
        }
    }
}

using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.HR.Controllers
{
    public class JobOpeningController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        public JobOpeningController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<JobOpening> objJobOpeningList = _unitOfWork.JobOpening.GetAll(jo => jo.Active == true, includeProperties: "Department,JobPosition");
            return View(objJobOpeningList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            JobOpeningViewModel jobOpeningVM = new()
            {
                JobOpening = new(),
                DepartmenList = _unitOfWork.Department.GetAll(d=>d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                JobPositionList = _unitOfWork.JobPosition.GetAll(jp=>jp.Active==true).Select(e => new SelectListItem
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

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(JobOpeningViewModel obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\jobopening");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.JobOpening.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.JobOpening.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.JobOpening.ImageUrl = @"\img\jobopening\" + fileName + extension;
                }

                if (obj.JobOpening.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.JobOpening.CreatedBy = ext;
                    obj.JobOpening.CreatedDateTime = DateTime.Now;
                    _unitOfWork.JobOpening.Add(obj.JobOpening);
                    TempData["success"] = "Job Opening created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.JobOpening.UpdatedBy = ext;
                    obj.JobOpening.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.JobOpening.Update(obj.JobOpening);
                    TempData["success"] = "Job Opening updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj.JobOpening);
        }


        public IActionResult ChangeStatus(int? id)
        {
            JobOpening jobOpening = _unitOfWork.JobOpening.GetFirstOrDefault(jo => jo.Id == id);
            if (jobOpening == null)
            {
                return NotFound();
            }
            _unitOfWork.JobOpening.ChangeStatus(jobOpening);
            _unitOfWork.Save();
            TempData["success"] = "Job Opening removed successfully";
            return RedirectToAction("Index");

        }

    }
}

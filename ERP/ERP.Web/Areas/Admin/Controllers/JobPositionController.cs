using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.Web.Controllers
{
    
    public class JobPositionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public JobPositionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<JobPosition> objJobPositionList = _unitOfWork.JobPosition.GetAll(j => j.Active == true);
            return View(objJobPositionList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(JobPosition jp)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);

                jp.CreatedBy = ext;
                jp.CreatedDateTime = DateTime.Now;

                _unitOfWork.JobPosition.Add(jp);
            _unitOfWork.Save();
                TempData["success"] = "Job Position created successfully";
            return RedirectToAction("Index");

            }
            TempData["error"] = "Something were wrong creating the Job Position";
            return View(jp);
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            JobPosition jp = _unitOfWork.JobPosition.GetFirstOrDefault(d =>d.Id == id);
            if (jp == null)
            {
                return NotFound();
            }
            return View(jp);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(JobPosition jp)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);
                jp.UpdatedBy = ext;
                jp.UpdatedDateTime = DateTime.Now;

                _unitOfWork.JobPosition.Update(jp);
                _unitOfWork.Save();
                TempData["success"] = "Job Position updated successfully";
                return RedirectToAction("Index");

            }
            TempData["error"] = "Something were wrong editing the Job Position";
            return View(jp);
        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    JobPosition dpt = _unitOfWork.JobPosition.GetFirstOrDefault(d => d.Id == id);
        //    if (dpt == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(dpt);
        //}

        //[HttpPost,ActionName("Delete")]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult DeletePost(int? id)
        //{
        //    JobPosition jp = _unitOfWork.JobPosition.GetFirstOrDefault(d => d.Id == id);
        //    if (jp == null)
        //    {
        //        return NotFound();
        //    }

        //        _unitOfWork.JobPosition.Remove(jp);
        //        _unitOfWork.Save();
        //    TempData["success"] = "Job Position deleted successfully";
        //    return RedirectToAction("Index");

        //}

        public IActionResult ChangeStatus(int? id)
        {
            JobPosition jp = _unitOfWork.JobPosition.GetFirstOrDefault(d => d.Id == id);
            if (jp == null)
            {
                return NotFound();
            }
            _unitOfWork.JobPosition.ChangeStatus(jp);
            _unitOfWork.Save();
            TempData["success"] = "Job Position removed successfully";
            return RedirectToAction("Index");

        }
    }
}

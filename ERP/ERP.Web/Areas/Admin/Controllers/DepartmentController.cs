using ERP.DataAccess.Data;
using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class DepartmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Department> objDepartmentList = _unitOfWork.Department.GetAll(d=> d.Active == true);
            return View(objDepartmentList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Department dpt)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);

                dpt.CreatedBy = ext;
                dpt.CreatedDateTime = DateTime.Now;
                _unitOfWork.Department.Add(dpt);
            _unitOfWork.Save();
                TempData["success"] = "Department created successfully";
            return RedirectToAction("Index");

            }
            TempData["error"] = "Something were wrong creating the Department";
            return View(dpt);
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Department dpt = _unitOfWork.Department.GetFirstOrDefault(d =>d.Id == id);
            if (dpt == null)
            {
                return NotFound();
            }
            return View(dpt);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Department dpt)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);
                dpt.UpdatedBy = ext;
                dpt.UpdatedDateTime = DateTime.Now;

                _unitOfWork.Department.Update(dpt);
                _unitOfWork.Save();
                TempData["success"] = "Department updated successfully";
                return RedirectToAction("Index");

            }
            TempData["error"] = "Something were wrong editing the Department";
            return View(dpt);
        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Department dpt = _unitOfWork.Department.GetFirstOrDefault(d => d.Id == id);
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
        //    Department dpt = _unitOfWork.Department.GetFirstOrDefault(d => d.Id == id);
        //    if (dpt == null)
        //    {
        //        return NotFound();
        //    }

        //        _unitOfWork.Department.Remove(dpt);
        //        _unitOfWork.Save();
        //    TempData["success"] = "Department deleted successfully";
        //    return RedirectToAction("Index");

        //}

        public IActionResult ChangeStatus(int? id)
        {
            Department dpt = _unitOfWork.Department.GetFirstOrDefault(d => d.Id == id);
            if (dpt == null)
            {
                return NotFound();
            }
            _unitOfWork.Department.ChangeStatus(dpt);
            _unitOfWork.Save();
            TempData["success"] = "Department removed successfully";
            return RedirectToAction("Index");

        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Department dpt = _unitOfWork.Department.GetFirstOrDefault(d => d.Id == id);
            if (dpt == null)
            {
                return NotFound();
            }
            return View(dpt);
        }
    }
}

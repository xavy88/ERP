using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using ERP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Service> objServiceList = _unitOfWork.Service.GetAll(e => e.Active == true, includeProperties: "Department");
            return View(objServiceList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ServiceViewModel servVM = new()
            {
                Service = new(),
                DepartmenList = _unitOfWork.Department.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
            };

            if (id == null || id == 0)
            {
                //Create
                return View(servVM);
            }
            else
            {
                //Update
                servVM.Service = _unitOfWork.Service.GetFirstOrDefault(e => e.Id == id);
                return View(servVM);
            }
            return View(servVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ServiceViewModel obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.Service.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.Service.CreatedBy = ext;
                    obj.Service.CreatedDateTime = DateTime.Now;
                    _unitOfWork.Service.Add(obj.Service);
                    TempData["success"] = "Service created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.Service.UpdatedBy = ext;
                    obj.Service.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.Service.Update(obj.Service);
                    TempData["success"] = "Service updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj.Service);
        }

        public IActionResult ChangeStatus(int? id)
        {
            Service serv = _unitOfWork.Service.GetFirstOrDefault(d => d.Id == id);
            if (serv == null)
            {
                return NotFound();
            }
            _unitOfWork.Service.ChangeStatus(serv);
            _unitOfWork.Save();
            TempData["success"] = "Service removed successfully";
            return RedirectToAction("Index");

        }

        //GET
        public IActionResult Details(int? id)
        {
            ServiceViewModel servVM = new()
            {
                Service = new(),
                DepartmenList = _unitOfWork.Department.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
            };

          
                servVM.Service = _unitOfWork.Service.GetFirstOrDefault(e => e.Id == id);
                return View(servVM);
        
        }
    }
}

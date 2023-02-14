using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.Manager.Controllers
{
    public class ClientAssigmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientAssigmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<ClientAssigment> objClientAssigmentList = _unitOfWork.ClientAssigment.GetAll(includeProperties: "Client,Department,JobPosition,Employee");
            return View(objClientAssigmentList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ClientAssigmentViewModel clientAssigmentVM = new()
            {
                ClientAssigment = new(),
                ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                {
                    Text = e.BusinessName,
                    Value = e.Id.ToString(),
                }),
                EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                DepartmentList = _unitOfWork.Department.GetAll(d => d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                JobPositionList = _unitOfWork.JobPosition.GetAll(d => d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
            };

            if (id == null || id == 0)
            {
                //Create
                return View(clientAssigmentVM);
            }
            else
            {
                //Update
                clientAssigmentVM.ClientAssigment = _unitOfWork.ClientAssigment.GetFirstOrDefault(ca => ca.Id == id);
                return View(clientAssigmentVM);
            }
            return View(clientAssigmentVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ClientAssigmentViewModel obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.ClientAssigment.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.ClientAssigment.CreatedBy = ext;
                    obj.ClientAssigment.CreatedDateTime = DateTime.Now;
                    _unitOfWork.ClientAssigment.Add(obj.ClientAssigment);
                    TempData["success"] = "Client assigned successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.ClientAssigment.UpdatedBy = ext;
                    obj.ClientAssigment.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.ClientAssigment.Update(obj.ClientAssigment);
                    TempData["success"] = "Assignation updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj.ClientAssigment);
        }

        public IActionResult Delete(int? id)
        {
            ClientAssigment clientAssigment = _unitOfWork.ClientAssigment.GetFirstOrDefault(ca => ca.Id == id);
            if (clientAssigment == null)
            {
                return NotFound();
            }

            _unitOfWork.ClientAssigment.Remove(clientAssigment);
            _unitOfWork.Save();
            TempData["success"] = "Assigment deleted successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {
            ClientAssigmentViewModel clientAssigmentVM = new()
            {
                ClientAssigment = new(),
                ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                {
                    Text = e.BusinessName,
                    Value = e.Id.ToString(),
                }),
                EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                DepartmentList = _unitOfWork.Department.GetAll(d => d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                JobPositionList = _unitOfWork.JobPosition.GetAll(d => d.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
            };

                clientAssigmentVM.ClientAssigment = _unitOfWork.ClientAssigment.GetFirstOrDefault(ca => ca.Id == id);
                return View(clientAssigmentVM);
       
        }
    }
}

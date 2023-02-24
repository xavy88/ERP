using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using ERP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.Manager.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_App_Supervisor + "," + SD.Role_Multimedia_Supervisor + "," + SD.Role_PPC_Supervisor + "," + SD.Role_Sales_Supervisor + "," + SD.Role_SEO_Supervisor + "," + SD.Role_Social_Media_Supervisor + "," + SD.Role_Web_Supervisor)]
    public class TasksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Tasks> objTasksList = _unitOfWork.Tasks.GetAll(e => e.Active == true, includeProperties: "Department,JobPosition");
            return View(objTasksList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            TasksViewModel tasksVM = new()
            {
                Tasks = new(),
                DepartmenList = _unitOfWork.Department.GetAll(d=>d.Active== true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                JobPositionList = _unitOfWork.JobPosition.GetAll(jp=>jp.Active ==true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
            };

            if (id == null || id == 0)
            {
                //Create
                return View(tasksVM);
            }
            else
            {
                //Update
                tasksVM.Tasks = _unitOfWork.Tasks.GetFirstOrDefault(e => e.Id == id);
                return View(tasksVM);
            }
            return View(tasksVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TasksViewModel obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.Tasks.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.Tasks.CreatedBy = ext;
                    obj.Tasks.CreatedDateTime = DateTime.Now;
                    _unitOfWork.Tasks.Add(obj.Tasks);
                    TempData["success"] = "Task created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.Tasks.UpdatedBy = ext;
                    obj.Tasks.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.Tasks.Update(obj.Tasks);
                    TempData["success"] = "Task updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj.Tasks);
        }

        public IActionResult ChangeStatus(int? id)
        {
            Tasks tasks = _unitOfWork.Tasks.GetFirstOrDefault(d => d.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }
            _unitOfWork.Tasks.ChangeStatus(tasks);
            _unitOfWork.Save();
            TempData["success"] = "Task removed successfully";
            return RedirectToAction("Index");

        }

        //GET
        public IActionResult Details(int? id)
        {
            TasksViewModel tasksVM = new()
            {
                Tasks = new(),
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

               tasksVM.Tasks = _unitOfWork.Tasks.GetFirstOrDefault(e => e.Id == id);
                return View(tasksVM);
        }
    }
}

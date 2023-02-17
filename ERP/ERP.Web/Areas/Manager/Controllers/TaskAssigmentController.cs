using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.Manager.Controllers
{
    public class TaskAssigmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskAssigmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
            string str = claim.ToString();
            string ext = str.Remove(0, 60);

            IEnumerable<TaskAssigment> objTaskAssigmentList = _unitOfWork.TaskAssigment.GetAll(e => e.Closed == false && e.Employee.Email == ext, includeProperties: "Tasks,Client,Employee");
            return View(objTaskAssigmentList);
        }

        public IActionResult TaskAssigmentList(string status)
        {
            IEnumerable<TaskAssigment> objTaskAssigmentList = _unitOfWork.TaskAssigment.GetAll(includeProperties: "Tasks,Client,Employee");

            switch (status)
            {
                case "true":
                    objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true);
                    break;
                case "false":
                    objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false);
                    break;
                default:
                    break;
            }
            return View(objTaskAssigmentList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            TaskAssigmentViewModel taskAssigmentVM = new()
            {
                TaskAssigment = new(),
                TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                {
                    Text = e.BusinessName,
                    Value = e.Id.ToString(),
                }),
            };

            if (id == null || id == 0)
            {
                //Create
                return View(taskAssigmentVM);
            }
            else
            {
                //Update
                taskAssigmentVM.TaskAssigment = _unitOfWork.TaskAssigment.GetFirstOrDefault(ta => ta.Id == id);
                return View(taskAssigmentVM);
            }
            return View(taskAssigmentVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TaskAssigmentViewModel obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.TaskAssigment.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.TaskAssigment.CreatedBy = ext;
                    obj.TaskAssigment.CreatedDateTime = DateTime.Now;
                    _unitOfWork.TaskAssigment.Add(obj.TaskAssigment);
                    TempData["success"] = "Task assigned successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.TaskAssigment.UpdatedBy = ext;
                    obj.TaskAssigment.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.TaskAssigment.Update(obj.TaskAssigment);
                    TempData["success"] = "Assignation updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj.TaskAssigment);
        }

        public IActionResult ChangeStatus(int? id)
        {
            TaskAssigment taskAssigment = _unitOfWork.TaskAssigment.GetFirstOrDefault(d => d.Id == id);
            if (taskAssigment == null)
            {
                return NotFound();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
            string str = claim.ToString();
            string ext = str.Remove(0, 60);
            taskAssigment.ClosedBy = ext;
            taskAssigment.ClosedDateTime = DateTime.Now;

            _unitOfWork.TaskAssigment.ChangeStatus(taskAssigment);
            _unitOfWork.Save();
            TempData["success"] = "Task Assigment removed successfully";
            return RedirectToAction("Index");

        }

        public IActionResult Details(int? id)
        {
            TaskAssigmentViewModel taskAssigmentVM = new()
            {
                TaskAssigment = new(),
                TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                {
                    Text = e.BusinessName,
                    Value = e.Id.ToString(),
                }),
            };
                taskAssigmentVM.TaskAssigment = _unitOfWork.TaskAssigment.GetFirstOrDefault(ta => ta.Id == id);
                return View(taskAssigmentVM);
     
        }
    }
}

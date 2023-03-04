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
    [Authorize]
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

            IEnumerable<TaskAssigment> objTaskAssigmentList = _unitOfWork.TaskAssigment.GetAll(e => e.Closed == false && e.Employee.WorkEmail == ext, includeProperties: "Tasks,Client,Employee");
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

        public IActionResult TaskAssigmentByDpt(string status)
        {
            IEnumerable<TaskAssigment> objTaskAssigmentList = _unitOfWork.TaskAssigment.GetAll(includeProperties: "Tasks,Client,Employee");

            if (User.IsInRole(SD.Role_App) || User.IsInRole(SD.Role_App_Supervisor))
            {
                switch (status)
                {
                    case "true":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true && o.Employee.Department.Id ==11);
                        break;
                    case "false":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false && o.Employee.DepartmentId == 11);
                        break;
                    default:
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Employee.DepartmentId == 11);
                        break;
                }
                return View(objTaskAssigmentList);
            }

            else if(User.IsInRole(SD.Role_Multimedia) || User.IsInRole(SD.Role_Multimedia_Supervisor))
            {
                switch (status)
                {
                    case "true":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true && o.Employee.DepartmentId == 10);
                        break;
                    case "false":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false && o.Employee.DepartmentId == 10);
                        break;
                    default:
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Employee.DepartmentId == 10);
                        break;
                }
                return View(objTaskAssigmentList);
            }

            else if(User.IsInRole(SD.Role_PPC) || User.IsInRole(SD.Role_PPC_Supervisor))
            {
                switch (status)
                {
                    case "true":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true && o.Employee.DepartmentId == 4);
                        break;
                    case "false":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false && o.Employee.DepartmentId == 4);
                        break;
                    default:
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Employee.DepartmentId == 4);
                        break;
                }
                return View(objTaskAssigmentList);
            }

            else if(User.IsInRole(SD.Role_Sales) || User.IsInRole(SD.Role_Sales_Supervisor))
            {
                switch (status)
                {
                    case "true":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true && o.Employee.DepartmentId == 12);
                        break;
                    case "false":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false && o.Employee.DepartmentId == 12);
                        break;
                    default:
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Employee.DepartmentId == 12);
                        break;
                }
                return View(objTaskAssigmentList);
            }

            else if(User.IsInRole(SD.Role_SEO) || User.IsInRole(SD.Role_SEO_Supervisor))
            {
                switch (status)
                {
                    case "true":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true && o.Employee.DepartmentId == 2);
                        break;
                    case "false":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false && o.Employee.DepartmentId == 2);
                        break;
                    default:
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Employee.DepartmentId == 2);
                        break;
                }
                return View(objTaskAssigmentList);
            }

            else if(User.IsInRole(SD.Role_Social_Media) || User.IsInRole(SD.Role_Social_Media_Supervisor))
            {
                switch (status)
                {
                    case "true":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true && o.Employee.DepartmentId == 7);
                        break;
                    case "false":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false && o.Employee.DepartmentId == 7);
                        break;
                    default:
                        objTaskAssigmentList = objTaskAssigmentList.Where(o =>o.Employee.DepartmentId == 7);
                        break;
                }
                return View(objTaskAssigmentList);
            }

            else if(User.IsInRole(SD.Role_Web) || User.IsInRole(SD.Role_Web_Supervisor))
            {
                switch (status)
                {
                    case "true":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == true && o.Employee.DepartmentId == 8);
                        break;
                    case "false":
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Closed == false && o.Employee.DepartmentId == 8);
                        break;
                    default:
                        objTaskAssigmentList = objTaskAssigmentList.Where(o => o.Employee.DepartmentId == 8);
                        break;
                }
                return View(objTaskAssigmentList);
            }

            else
            {
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

        }

        //GET
        public IActionResult Upsert(int? id)
        {
            if (User.IsInRole(SD.Role_App) || User.IsInRole(SD.Role_App_Supervisor))
            {
                TaskAssigmentViewModel taskAssigmentVM = new()
                {

                    TaskAssigment = new(),
                    TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true && t.Department.Name == "App").Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "App").Select(e => new SelectListItem
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
            }

            else if (User.IsInRole(SD.Role_Multimedia) || User.IsInRole(SD.Role_Multimedia_Supervisor))
            {
                TaskAssigmentViewModel taskAssigmentVM = new()
                {

                    TaskAssigment = new(),
                    TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true && t.Department.Name == "Multimedia").Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Multimedia").Select(e => new SelectListItem
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
            }

            else if (User.IsInRole(SD.Role_PPC) || User.IsInRole(SD.Role_PPC_Supervisor))
            {
                TaskAssigmentViewModel taskAssigmentVM = new()
                {

                    TaskAssigment = new(),
                    TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true && t.Department.Name == "PPC").Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "PPC").Select(e => new SelectListItem
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
            }

            else if (User.IsInRole(SD.Role_Sales) || User.IsInRole(SD.Role_Sales_Supervisor))
            {
                TaskAssigmentViewModel taskAssigmentVM = new()
                {

                    TaskAssigment = new(),
                    TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true && t.Department.Name == "Sales").Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Sales").Select(e => new SelectListItem
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
            }

            else if (User.IsInRole(SD.Role_SEO) || User.IsInRole(SD.Role_SEO_Supervisor))
            {
                TaskAssigmentViewModel taskAssigmentVM = new()
                {

                    TaskAssigment = new(),
                    TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true && t.Department.Name == "SEO").Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "SEO").Select(e => new SelectListItem
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
            }

            else if (User.IsInRole(SD.Role_Social_Media) || User.IsInRole(SD.Role_Social_Media_Supervisor))
            {
                TaskAssigmentViewModel taskAssigmentVM = new()
                {

                    TaskAssigment = new(),
                    TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true && t.Department.Name == "Social Media").Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Social Media").Select(e => new SelectListItem
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
            }

            else if (User.IsInRole(SD.Role_Web) || User.IsInRole(SD.Role_Web_Supervisor))
            {
                TaskAssigmentViewModel taskAssigmentVM = new()
                {

                    TaskAssigment = new(),
                    TasksList = _unitOfWork.Tasks.GetAll(t => t.Active == true && t.Department.Name == "Web").Select(e => new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Web").Select(e => new SelectListItem
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
            }

            else
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
            }
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

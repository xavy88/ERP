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
    
    public class ClientAssigmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientAssigmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);

                IEnumerable<ClientAssigment> objClientAssigmentList = _unitOfWork.ClientAssigment.GetAll(c=>c.Client.Active == true && c.Employee.WorkEmail == ext,includeProperties: "Client,Department,JobPosition,Employee");
                return View(objClientAssigmentList);
            
        }
        [Authorize]
        public IActionResult ActiveClientAssigment(string dpto)
        {
            IEnumerable<ClientAssigment> objClientAssigmentList = _unitOfWork.ClientAssigment.GetAll(c => c.Client.Active == true, includeProperties: "Client,Department,JobPosition,Employee");

            switch (dpto)
            {
                case "SEO":
                    objClientAssigmentList = objClientAssigmentList.Where(o => o.Department.Name == dpto);
                    break;
                case "Web":
                    objClientAssigmentList = objClientAssigmentList.Where(o => o.Department.Name == dpto);
                    break;
                case "PPC":
                    objClientAssigmentList = objClientAssigmentList.Where(o => o.Department.Name == dpto);
                    break;
                case "App":
                    objClientAssigmentList = objClientAssigmentList.Where(o => o.Department.Name == dpto);
                    break;
                case "Multimedia":
                    objClientAssigmentList = objClientAssigmentList.Where(o => o.Department.Name == dpto);
                    break;
                case "Social Media":
                    objClientAssigmentList = objClientAssigmentList.Where(o => o.Department.Name == dpto);
                    break;
                default:
                    break;
            }
            return View(objClientAssigmentList);
        }
        //GET
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_App_Supervisor + "," + SD.Role_Multimedia_Supervisor + "," + SD.Role_PPC_Supervisor + "," + SD.Role_Sales_Supervisor + "," + SD.Role_SEO_Supervisor + "," + SD.Role_Social_Media_Supervisor + "," + SD.Role_Web_Supervisor)]
        public IActionResult Upsert(int? id)
        {
            #region deleteme
            //ClientAssigmentViewModel clientAssigmentVM = new()
            //{
            //    ClientAssigment = new(),
            //    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
            //    {
            //        Text = e.BusinessName,
            //        Value = e.Id.ToString(),
            //    }),
            //    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true).Select(e => new SelectListItem
            //    {
            //        Text = e.Name,
            //        Value = e.Id.ToString(),
            //    }),
            //    DepartmentList = _unitOfWork.Department.GetAll(d => d.Active == true).Select(e => new SelectListItem
            //    {
            //        Text = e.Name,
            //        Value = e.Id.ToString(),
            //    }),
            //    JobPositionList = _unitOfWork.JobPosition.GetAll(d => d.Active == true).Select(e => new SelectListItem
            //    {
            //        Text = e.Name,
            //        Value = e.Id.ToString(),
            //    }),
            //};

            //if (id == null || id == 0)
            //{
            //    //Create
            //    return View(clientAssigmentVM);
            //}
            //else
            //{
            //    //Update
            //    clientAssigmentVM.ClientAssigment = _unitOfWork.ClientAssigment.GetFirstOrDefault(ca => ca.Id == id);
            //    return View(clientAssigmentVM);
            //}
            //return View(clientAssigmentVM);
            #endregion

            if (User.IsInRole(SD.Role_App) || User.IsInRole(SD.Role_App_Supervisor))
            {
                ClientAssigmentViewModel clientAssigmentVM = new()
                {
                    ClientAssigment = new(),
                    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                    {
                        Text = e.BusinessName,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name=="App").Select(e => new SelectListItem
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

            else if (User.IsInRole(SD.Role_Multimedia) || User.IsInRole(SD.Role_Multimedia_Supervisor))
            {
                ClientAssigmentViewModel clientAssigmentVM = new()
                {
                    ClientAssigment = new(),
                    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                    {
                        Text = e.BusinessName,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Multimedia").Select(e => new SelectListItem
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

            else if (User.IsInRole(SD.Role_PPC) || User.IsInRole(SD.Role_PPC_Supervisor))
            {
                ClientAssigmentViewModel clientAssigmentVM = new()
                {
                    ClientAssigment = new(),
                    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                    {
                        Text = e.BusinessName,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "PPC").Select(e => new SelectListItem
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

            else if (User.IsInRole(SD.Role_Sales) || User.IsInRole(SD.Role_Sales_Supervisor))
            {
                ClientAssigmentViewModel clientAssigmentVM = new()
                {
                    ClientAssigment = new(),
                    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                    {
                        Text = e.BusinessName,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Sales").Select(e => new SelectListItem
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

            else if (User.IsInRole(SD.Role_SEO) || User.IsInRole(SD.Role_SEO_Supervisor))
            {
                ClientAssigmentViewModel clientAssigmentVM = new()
                {
                    ClientAssigment = new(),
                    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                    {
                        Text = e.BusinessName,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "SEO").Select(e => new SelectListItem
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

            else if (User.IsInRole(SD.Role_Social_Media) || User.IsInRole(SD.Role_Social_Media_Supervisor))
            {
                ClientAssigmentViewModel clientAssigmentVM = new()
                {
                    ClientAssigment = new(),
                    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                    {
                        Text = e.BusinessName,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Social Media").Select(e => new SelectListItem
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

            else if (User.IsInRole(SD.Role_Web) || User.IsInRole(SD.Role_Web_Supervisor))
            {
                ClientAssigmentViewModel clientAssigmentVM = new()
                {
                    ClientAssigment = new(),
                    ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                    {
                        Text = e.BusinessName,
                        Value = e.Id.ToString(),
                    }),
                    EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true && e.Department.Name == "Web").Select(e => new SelectListItem
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

            else
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
        }

        //POST
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_App_Supervisor + "," + SD.Role_Multimedia_Supervisor + "," + SD.Role_PPC_Supervisor + "," + SD.Role_Sales_Supervisor + "," + SD.Role_SEO_Supervisor + "," + SD.Role_Social_Media_Supervisor + "," + SD.Role_Web_Supervisor)]
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
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_App_Supervisor + "," + SD.Role_Multimedia_Supervisor + "," + SD.Role_PPC_Supervisor + "," + SD.Role_Sales_Supervisor + "," + SD.Role_SEO_Supervisor + "," + SD.Role_Social_Media_Supervisor + "," + SD.Role_Web_Supervisor)]
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
        [Authorize]
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

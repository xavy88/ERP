﻿using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ERP.Utility;

namespace ERP.Web.Areas.HR.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HR)]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> objEmployeeList = _unitOfWork.Employee.GetAll(e=>e.Active == true,includeProperties:"Department,jobPosition");
            return View(objEmployeeList);
        }

        public IActionResult AllEmployee(string dpto)
        {
            IEnumerable<Employee> objEmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true, includeProperties: "Department,jobPosition");

            switch (dpto)
            {
                case "SEO":
                    objEmployeeList = objEmployeeList.Where(o => o.Department.Name == dpto);
                    break;
                case "Web":
                    objEmployeeList = objEmployeeList.Where(o => o.Department.Name == dpto);
                    break;
                case "PPC":
                    objEmployeeList = objEmployeeList.Where(o => o.Department.Name == dpto);
                    break;
                case "App":
                    objEmployeeList = objEmployeeList.Where(o => o.Department.Name == dpto);
                    break;
                case "Multimedia":
                    objEmployeeList = objEmployeeList.Where(o => o.Department.Name == dpto);
                    break;
                case "Social Media":
                    objEmployeeList = objEmployeeList.Where(o => o.Department.Name == dpto);
                    break;
                default:
                    break;
            }

            return View(objEmployeeList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            EmployeeViewModel employeeVM = new()
            {
                Employee = new(),
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

            if (id==null || id ==0)
            {
                //Create
                return View(employeeVM);
            }
            else
            {
                //Update
                employeeVM.Employee = _unitOfWork.Employee.GetFirstOrDefault(e => e.Id == id);
                return View(employeeVM);
            }
            return View(employeeVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EmployeeViewModel obj, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\employees");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Employee.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Employee.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Employee.ImageUrl = @"\img\employees\" + fileName + extension;
                }

                if (obj.Employee.Id== 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.Employee.CreatedBy = ext;
                    obj.Employee.CreatedDateTime = DateTime.Now;
                    _unitOfWork.Employee.Add(obj.Employee);
                    TempData["success"] = "Employee created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.Employee.UpdatedBy = ext;
                    obj.Employee.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.Employee.Update(obj.Employee);
                    TempData["success"] = "Employee updated successfully";
                }
                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            return View(obj.Employee);
        }

        public IActionResult ChangeStatus(int? id)
        {
            Employee emp = _unitOfWork.Employee.GetFirstOrDefault(d => d.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            _unitOfWork.Employee.ChangeStatus(emp);
            _unitOfWork.Save();
            TempData["success"] = "Employee removed successfully";
            return RedirectToAction("Index");

        }
        //GET
        public IActionResult Details(int? id)
        {
            EmployeeViewModel employeeVM = new()
            {
                Employee = new(),
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

              employeeVM.Employee = _unitOfWork.Employee.GetFirstOrDefault(e => e.Id == id);
               return View(employeeVM);
           
        }
    }
}

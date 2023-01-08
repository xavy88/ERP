using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Web.Areas.HR.Controllers
{
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

        //GET
        public IActionResult Upsert(int? id)
        {
            EmployeeViewModel employeeVM = new()
            {
                Employee = new(),
                DepartmenList = _unitOfWork.Department.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                JobPositionList = _unitOfWork.JobPosition.GetAll().Select(e => new SelectListItem
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
                    _unitOfWork.Employee.Add(obj.Employee);
                }
                else
                {
                    _unitOfWork.Employee.Update(obj.Employee);
                }
                _unitOfWork.Save();
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }
            return View(obj.Employee);
        }

        //Get
        //public IActionResult Delete(int? id)
        //{
        //    EmployeeViewModel employeeVM = new()
        //    {
        //        Employee = new(),
        //        DepartmenList = _unitOfWork.Department.GetAll().Select(e => new SelectListItem
        //        {
        //            Text = e.Name,
        //            Value = e.Id.ToString(),
        //        }),
        //        JobPositionList = _unitOfWork.JobPosition.GetAll().Select(e => new SelectListItem
        //        {
        //            Text = e.Name,
        //            Value = e.Id.ToString(),
        //        }),
        //    };


        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    employeeVM.Employee = _unitOfWork.Employee.GetFirstOrDefault(d => d.Id == id);
        //    if (employeeVM == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(employeeVM);
        //}

        //[HttpPost, ActionName("Delete")]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult DeletePost(int? id)
        //{
        //    Employee emp = _unitOfWork.Employee.GetFirstOrDefault(d => d.Id == id);
        //    if (emp == null)
        //    {
        //        return NotFound();
        //    }

        //    var oldImagePath = Path.Combine(_hostEnviroment.WebRootPath, emp.ImageUrl.TrimStart('\\'));
        //    if (System.IO.File.Exists(oldImagePath))
        //    {
        //        System.IO.File.Delete(oldImagePath);
        //    }

        //    _unitOfWork.Employee.Remove(emp);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Employee deleted successfully";
        //    return RedirectToAction("Index");

        //}

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

    }
}

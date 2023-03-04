using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using ERP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.HR.Controllers
{
    
    public class PayRollDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public PayRollDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_HR))
            {
                IEnumerable<PayRollDetail> objPayRollDetailList = _unitOfWork.PayRollDetail.GetAll(prd => prd.Paid == false, includeProperties: "Employee,PayRollOrder");
                return View(objPayRollDetailList);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);

                IEnumerable<PayRollDetail> objPayRollDetailList = _unitOfWork.PayRollDetail.GetAll(prd => prd.PayRollOrder.Closed == false && prd.Employee.WorkEmail == ext, includeProperties: "Employee,PayRollOrder");
                return View(objPayRollDetailList);

            }
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HR)]
        public IActionResult RollDetails(int? id)
        {
            IEnumerable<PayRollDetail> objPayRollDetailList = _unitOfWork.PayRollDetail.GetAll(prd =>prd.PayRollOrderId == id, includeProperties: "Employee,PayRollOrder");
            return View(objPayRollDetailList);
        }

        //GET
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HR)]
        public IActionResult Upsert(int? id)
        {
            PayRollDetailViewModel payRollDetailVM = new()
            {
                PayRollDetail = new(),
                EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                PayRollOrderList = _unitOfWork.PayRollOrder.GetAll(pr => pr.Paid == false).Select(e => new SelectListItem
                {
                    Text = e.Reference,
                    Value = e.Id.ToString(),
                }),
            };

            if (id==null || id ==0)
            {
                //Create
                return View(payRollDetailVM);
            }
            else
            {
                //Update
                payRollDetailVM.PayRollDetail = _unitOfWork.PayRollDetail.GetFirstOrDefault(e => e.Id == id);
                return View(payRollDetailVM);
            }
            return View(payRollDetailVM);
        }

        //POST
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PayRollDetailViewModel obj)
        {
            
            if (ModelState.IsValid)
            {

                if (obj.PayRollDetail.Id== 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.PayRollDetail.CreatedBy = ext;
                    obj.PayRollDetail.CreatedDateTime = DateTime.Now;
                    _unitOfWork.PayRollDetail.Add(obj.PayRollDetail);
                    TempData["success"] = "Pay Roll Detail created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.PayRollDetail.UpdatedBy = ext;
                    obj.PayRollDetail.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.PayRollDetail.Update(obj.PayRollDetail);
                    TempData["success"] = "Pay Roll Detail updated successfully";
                }
                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            return View(obj.PayRollDetail);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HR)]
        public IActionResult Paid(int? id)
        {
            PayRollDetail prd = _unitOfWork.PayRollDetail.GetFirstOrDefault(prd => prd.Id == id);
            if (prd == null)
            {
                return NotFound();
            }
            _unitOfWork.PayRollDetail.Paid(prd);
            _unitOfWork.Save();
            TempData["success"] = "Pay Roll Detail removed successfully";
            return RedirectToAction("Index");

        }
        [Authorize]
        public IActionResult Details(int? id)
        {
            PayRollDetailViewModel payRollDetailVM = new()
            {
                PayRollDetail = new(),
                EmployeeList = _unitOfWork.Employee.GetAll(e => e.Active == true).Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                }),
                PayRollOrderList = _unitOfWork.PayRollOrder.GetAll(pr => pr.Paid == false).Select(e => new SelectListItem
                {
                    Text = e.Reference,
                    Value = e.Id.ToString(),
                }),
            };
                       
                payRollDetailVM.PayRollDetail = _unitOfWork.PayRollDetail.GetFirstOrDefault(e => e.Id == id);
                return View(payRollDetailVM);
        }
    }
}

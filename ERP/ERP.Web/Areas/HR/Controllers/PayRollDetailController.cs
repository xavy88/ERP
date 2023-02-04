﻿using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
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
        public IActionResult Index()
        {
            IEnumerable<PayRollDetail> objPayRollDetailList = _unitOfWork.PayRollDetail.GetAll(prd=>prd.Paid == false,includeProperties:"Employee,PayRollOrder");
            return View(objPayRollDetailList);
        }

        //GET
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

    }
}
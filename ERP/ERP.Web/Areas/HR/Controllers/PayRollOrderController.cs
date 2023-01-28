﻿using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.HR.Controllers
{
    public class PayRollOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PayRollOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            IEnumerable<PayRollOrder> objPayRollOrderList = _unitOfWork.PayRollOrder.GetAll(pr => pr.Closed == false);
            return View(objPayRollOrderList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            PayRollOrder payRollOrder = new PayRollOrder();
            
            if (id==null || id ==0)
            {
                //Create
                return View(payRollOrder);
            }
            else
            {
                //Update
                payRollOrder = _unitOfWork.PayRollOrder.GetFirstOrDefault(pr => pr.Id == id);
                return View(payRollOrder);
            }
            return View(payRollOrder);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PayRollOrder obj)
        {
            
            if (ModelState.IsValid)
            {
                if (obj.Id== 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.CreatedBy = ext;
                    obj.CreatedDateTime = DateTime.Now;
                    _unitOfWork.PayRollOrder.Add(obj);
                    TempData["success"] = "Pay Roll Created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.UpdatedBy = ext;
                    obj.UpdatedDateTime = DateTime.Now;
                    _unitOfWork.PayRollOrder.Update(obj);
                    TempData["success"] = "Pay Roll updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Paid(int? id)
        {
            PayRollOrder payRollOrder = _unitOfWork.PayRollOrder.GetFirstOrDefault(pr => pr.Id == id);
            if (payRollOrder == null)
            {
                return NotFound();
            }
            _unitOfWork.PayRollOrder.Paid(payRollOrder);
            _unitOfWork.Save();
            TempData["success"] = "Pay Roll paid successfully";
            return RedirectToAction("Index");

        }

        public IActionResult Closed(int? id)
        {
            PayRollOrder payRollOrder = _unitOfWork.PayRollOrder.GetFirstOrDefault(pr => pr.Id == id);
            if (payRollOrder == null)
            {
                return NotFound();
            }
            _unitOfWork.PayRollOrder.Closed(payRollOrder);
            _unitOfWork.Save();
            TempData["success"] = "Pay Roll Closed successfully";
            return RedirectToAction("Index");

        }

    }
}

using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.Sales.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Order> objOrderList = _unitOfWork.Order.GetAll(o => o.Closed == false, includeProperties: "Service,Client,Employee");
            return View(objOrderList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            OrderViewModel orderVM = new()
            {
                Order = new(),
                ServiceList = _unitOfWork.Service.GetAll(s => s.Active == true).Select(e => new SelectListItem
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
                return View(orderVM);
            }
            else
            {
                //Update
                orderVM.Order = _unitOfWork.Order.GetFirstOrDefault(o => o.Id == id);
                return View(orderVM);
            }
            return View(orderVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(OrderViewModel obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.Order.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.Order.CreatedBy = ext;
                    obj.Order.CreatedDateTime = DateTime.Now;
                    _unitOfWork.Order.Add(obj.Order);
                    TempData["success"] = "Order created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.Order.UpdatedBy = ext;
                    obj.Order.UpdatedDateTime = DateTime.Now;

                    _unitOfWork.Order.Update(obj.Order);
                    TempData["success"] = "Order updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj.Order);
        }

        public IActionResult Close(int? id)
        {
            Order order = _unitOfWork.Order.GetFirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
            string str = claim.ToString();
            string ext = str.Remove(0, 60);
            order.UpdatedBy = ext;
            order.UpdatedDateTime = DateTime.Now;

            _unitOfWork.Order.Close(order);
            _unitOfWork.Save();
            TempData["success"] = "Order closed successfully";
            return RedirectToAction("Index");

        }

        public IActionResult Paid(int? id)
        {
            Order order = _unitOfWork.Order.GetFirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
            string str = claim.ToString();
            string ext = str.Remove(0, 60);
            order.UpdatedBy = ext;
            order.UpdatedDateTime = DateTime.Now;

            _unitOfWork.Order.Pay(order);
            _unitOfWork.Save();
            TempData["success"] = "Order paid successfully";
            return RedirectToAction("Index");

        }
    }
}

using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using ERP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe.Checkout;
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
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Sales_Supervisor)]
        public IActionResult Index()
        {
            IEnumerable<Order> objOrderList = _unitOfWork.Order.GetAll(o => o.Closed == false, includeProperties: "Service,Client,Employee");
            return View(objOrderList);

            if (User.IsInRole(SD.Role_Client))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);

                IEnumerable<Order> cliOrderList = _unitOfWork.Order.GetAll(o => o.Closed == false && o.Client.BusinessEmail == ext, includeProperties: "Service,Client,Employee");
                return View(cliOrderList);
            }
        }
        [Authorize]
        public IActionResult ActiveClient(string dpto)
        {
            IEnumerable<Order> objOrderList = _unitOfWork.Order.GetAll(o => o.Closed == false, includeProperties: "Service,Client,Employee");

            switch (dpto)
            {
                case "SEO":
                    objOrderList =objOrderList.Where(o => o.Service.Name == dpto);
                    break;
                case "Web":
                    objOrderList = objOrderList.Where(o => o.Service.Name == dpto);
                    break;
                case "PPC":
                    objOrderList = objOrderList.Where(o => o.Service.Name == dpto);
                    break;
                case "App":
                    objOrderList = objOrderList.Where(o => o.Service.Name == dpto);
                    break;
                case "Multimedia":
                    objOrderList = objOrderList.Where(o => o.Service.Name == dpto);
                    break;
                case "Social Media":
                    objOrderList = objOrderList.Where(o => o.Service.Name == dpto);
                    break;
                    default:
                    break;
            }
            return View(objOrderList);
        }


        //GET
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Sales_Supervisor)]
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
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Sales_Supervisor)]
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
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Sales_Supervisor)]
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
        [HttpGet]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Sales_Supervisor + "," + SD.Role_Client)]
        public IActionResult Paid(int? id)
        {
            Order order = _unitOfWork.Order.GetFirstOrDefault(o => o.Id == id, includeProperties: "Service,Client,Employee");
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
           

            #region Stripe Settings

            var domain = "https://localhost:44373/";
            //OrderViewModel orderVM = new();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"Sales/Order/OrderConfirmation?id={order.Id}",
                CancelUrl = domain + $"Sales/Order/index",

            };

            //foreach (var item in orderVM.Order)
            //{

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(order.Amount * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = order.Service.Name
                        },

                    },
                    Quantity = 1,
                };
             options.LineItems.Add(sessionLineItem);
           
            //}

            var service = new SessionService();
            Session session = service.Create(options);
            order.SessionId = session.Id;
            order.PaymentIntentId = session.PaymentIntentId;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            _unitOfWork.Order.Pay(order);
            _unitOfWork.Save();
            #endregion



        }
        [Authorize]
        public IActionResult OrderConfirmation(int id)
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

            orderVM.Order = _unitOfWork.Order.GetFirstOrDefault(o => o.Id == id, includeProperties: "Service,Client,Employee");
            return View(orderVM);
           
        }

        //GET
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Sales_Supervisor + "," + SD.Role_Client)]
        public IActionResult Details(int? id)
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
    }
}

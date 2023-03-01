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
    
    public class DayOffController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DayOffController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_HR) || User.IsInRole(SD.Role_Manager))
            {

                IEnumerable<DayOff> objDayOffList = _unitOfWork.DayOff.GetAll(d => d.Closed == false);
                return View(objDayOffList);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                string str = claim.ToString();
                string ext = str.Remove(0, 60);

                IEnumerable<DayOff> objDayOffList = _unitOfWork.DayOff.GetAll(d => d.Closed == false && d.Email == ext);
                return View(objDayOffList);
            }
        }

        //GET
        [Authorize]
        public IActionResult Upsert(int? id)
        {
            DayOff dayOff = new DayOff();
            
            if (id==null || id ==0)
            {
                //Create
                return View(dayOff);
            }
            else
            {
                //Update
                dayOff = _unitOfWork.DayOff.GetFirstOrDefault(d => d.Id == id);
                return View(dayOff);
            }
            return View(dayOff);
        }

        //POST
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DayOff obj)
        {
            
            if (ModelState.IsValid)
            {
                if (obj.Id== 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.Email = ext;
                    obj.CreatedBy = ext;
                    obj.CreatedDateTime = DateTime.Now;
                    _unitOfWork.DayOff.Add(obj);
                    TempData["success"] = "Day Off Requested successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.UpdatedBy = ext;
                    obj.UpdatedDateTime = DateTime.Now;
                    _unitOfWork.DayOff.Update(obj);
                    TempData["success"] = "Day Off updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HR)]
        public IActionResult Approved(int? id)
        {
            DayOff dayOff = _unitOfWork.DayOff.GetFirstOrDefault(d => d.Id == id);
            if (dayOff == null)
            {
                return NotFound();
            }
            _unitOfWork.DayOff.Approved(dayOff);
            _unitOfWork.Save();
            TempData["success"] = "Date Off approved successfully";
            return RedirectToAction("Index");

        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HR)]
        public IActionResult Closed(int? id)
        {
            DayOff dayOff = _unitOfWork.DayOff.GetFirstOrDefault(d => d.Id == id);
            if (dayOff == null)
            {
                return NotFound();
            }
            _unitOfWork.DayOff.Closed(dayOff);
            _unitOfWork.Save();
            TempData["success"] = "Date Off Closed successfully";
            return RedirectToAction("Index");

        }

        //GET
        [Authorize]
        public IActionResult Details(int? id)
        {
            DayOff dayOff = new DayOff();

            if (id == null || id == 0)
            {
                return NotFound();
            }
           
                dayOff = _unitOfWork.DayOff.GetFirstOrDefault(d => d.Id == id);
                return View(dayOff);
          
        }
    }
}

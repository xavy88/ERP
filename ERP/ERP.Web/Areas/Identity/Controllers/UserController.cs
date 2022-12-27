using ERP.DataAccess.Data;
using ERP.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Areas.Identity.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _db = db; 
        }
        public IActionResult Index()
        {
            var userList = _db.ApplicationUser.ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }
            return View(userList);
        }

        public IActionResult Edit(string userId)
        {
            var objFromDb = _db.ApplicationUser.FirstOrDefault(u=>u.Id== userId);
            if (objFromDb == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(u => u.UserId == objFromDb.Id);

            if (role !=null)
            {
                objFromDb.RoleId = roles.FirstOrDefault(u=>u.Id==role.RoleId).Id; 
            }
            objFromDb.RoleList = _db.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });

            return View(objFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            { 
                var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == user.Id);
                if (objFromDb == null)
                {
                    TempData["error"] = "User not found";
                    return NotFound();
                }
                var userRole = _db.UserRoles.FirstOrDefault(u=>u.UserId == objFromDb.Id);
                if (userRole !=null)
                {
                    var previousRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    //Removing the previous role
                    await _userManager.RemoveFromRoleAsync(objFromDb, previousRoleName);
                }
                //Adding the new one
                await _userManager.AddToRoleAsync(objFromDb, _db.Roles.FirstOrDefault(u => u.Id == user.RoleId).Name);
                objFromDb.Name = user.Name;
                _db.SaveChanges();
                TempData["success"] = "User has been edited successfully";
                return RedirectToAction(nameof(Index));
            }

            user.RoleList = _db.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });

            return View(user);
        }
        [HttpPost]
        public IActionResult LockUnlock(string userId)
        {
            var objFromDb = _db.ApplicationUser.FirstOrDefault(u=>u.Id== userId);
            if (objFromDb == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }
            if (objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd>DateTime.Now)
            {
                //User is locked and will remain locked untill lockuotend time 
                //clicking in this action will unlock them.

                objFromDb.LockoutEnd = DateTime.Now;
                TempData["success"] = "User unlocked successfully";
            }
            else
            {
                //user is not locked and we want to lock it.
                objFromDb.LockoutEnd= DateTime.Now.AddYears(10);
                TempData["success"] = "User locked successfully";
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var objFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            if (objFromDb == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }
            _db.ApplicationUser.Remove(objFromDb);
            _db.SaveChanges();
            TempData["success"] = "User deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}

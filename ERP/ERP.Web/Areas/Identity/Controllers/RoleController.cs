using ERP.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Areas.Identity.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolesManager;

        public RoleController(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolesManager)
        {
            _db = db;
            _rolesManager = rolesManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Upsert(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                var objFromDb = _db.Roles.FirstOrDefault(u=>u.Id == id);
                return View(objFromDb);
            }
                        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole roleObj)
        {
            if (await _rolesManager.RoleExistsAsync(roleObj.Name))
            {
                TempData["error"] = "Role already exists";
                return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrEmpty(roleObj.Id))
            {
                await _rolesManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
                TempData["success"] = "Role created successfully";
            }
            else
            {
                var objRoleFromDb = _db.Roles.FirstOrDefault(r => r.Id == roleObj.Id);
                if (objRoleFromDb == null)
                {
                    TempData["error"] = "Role not found";
                    return RedirectToAction(nameof(Index));
                }
                objRoleFromDb.Name = roleObj.Name;
                objRoleFromDb.NormalizedName = roleObj.Name.ToUpper();
                var result = await _rolesManager.UpdateAsync(objRoleFromDb);
                TempData["success"] = "Role updated successfully";
            }
            return RedirectToAction(nameof(Index));
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
           var objFromDb = _db.Roles.FirstOrDefault(r=>r.Id == id);
            if (objFromDb == null)
            {
                TempData["error"] = "Role not found";
                return RedirectToAction(nameof(Index));
            }
            var userRolesForThisRole = _db.UserRoles.Where(r => r.RoleId == id).Count();
            if (userRolesForThisRole > 0)
            {
                TempData["error"] = "Cannot delete this role, since there are users assigned to this role.";
                return RedirectToAction(nameof(Index));
            }
            await _rolesManager.DeleteAsync(objFromDb);
            TempData["success"] = "Role deleted successfully";
            return RedirectToAction(nameof(Index));

        }
    }
}

using ERP.Web.Data;
using ERP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly ApplicationDbContext _db;

        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Department> objDepartmentList = _db.Departments;
            return View(objDepartmentList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Department dpt)
        {
            if (ModelState.IsValid)
            {
            _db.Departments.Add(dpt);
            _db.SaveChanges();
            return RedirectToAction("Index");

            }

            return View(dpt);
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Department dpt = _db.Departments.FirstOrDefault(d =>d.Id == id);
            if (dpt == null)
            {
                return NotFound();
            }
            return View(dpt);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Department dpt)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Update(dpt);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(dpt);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Department dpt = _db.Departments.FirstOrDefault(d => d.Id == id);
            if (dpt == null)
            {
                return NotFound();
            }
            return View(dpt);
        }

        [HttpPost,ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Department dpt = _db.Departments.FirstOrDefault(d => d.Id == id);
            if (dpt == null)
            {
                return NotFound();
            }

                _db.Departments.Remove(dpt);
                _db.SaveChanges();
                return RedirectToAction("Index");

        }
    }
}

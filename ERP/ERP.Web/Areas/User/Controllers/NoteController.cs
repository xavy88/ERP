using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using ERP.Models.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.User.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<Note> objNoteList = _unitOfWork.Note.GetAll(n => n.ApplicationUserId == claim.Value,includeProperties: "Client,ApplicationUser");
            return View(objNoteList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            NoteViewModel noteVM = new()
            {
                Note = new(),
                ClientList = _unitOfWork.Client.GetAll(c => c.Active == true).Select(e => new SelectListItem
                {
                    Text = e.BusinessName,
                    Value = e.Id.ToString(),
                }),
               
            };

            if (id == null || id == 0)
            {
                //Create
                return View(noteVM);
            }
            else
            {
                //Update
                noteVM.Note = _unitOfWork.Note.GetFirstOrDefault(n => n.Id == id);
                return View(noteVM);
            }
            return View(noteVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(NoteViewModel obj)
        {

            if (ModelState.IsValid)
            {
                
                if (obj.Note.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                    obj.Note.ApplicationUserId = claim.Value;
                    obj.Note.CreatedDateTime = DateTime.Now;
                    _unitOfWork.Note.Add(obj.Note);
                    TempData["success"] = "Note created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                    obj.Note.ApplicationUserId = claim.Value;
                    obj.Note.CreatedDateTime = DateTime.Now;

                    _unitOfWork.Note.Update(obj.Note);
                    TempData["success"] = "Note updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj.Note);
        }

        public IActionResult Delete(int? id)
        {
            Note note = _unitOfWork.Note.GetFirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _unitOfWork.Note.Remove(note);
            _unitOfWork.Save();
            TempData["success"] = "Note deleted successfully";
            return RedirectToAction("Index");
        }

    }
}

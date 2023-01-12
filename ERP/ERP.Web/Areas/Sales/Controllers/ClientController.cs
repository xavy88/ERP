using ERP.DataAccess.Repository.IRepository;
using ERP.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.Web.Areas.Sales.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        public ClientController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Client> objClientList = _unitOfWork.Client.GetAll(e => e.Active == true);
            return View(objClientList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Client client = new Client();

            if (id == null || id == 0)
            {
                //Create
                return View(client);
            }
            else
            {
                //Update
                client = _unitOfWork.Client.GetFirstOrDefault(d => d.Id == id);
                return View(client);
            }
            return View(client);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Client obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\customer");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Logo != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Logo.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Logo = @"\img\customer\" + fileName + extension;
                }

                if (obj.Id == 0)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);

                    obj.CreatedBy = ext;
                    obj.CreatedDateTime = DateTime.Now;
                    _unitOfWork.Client.Add(obj);
                    TempData["success"] = "Client created successfully";
                }
                else
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    string str = claim.ToString();
                    string ext = str.Remove(0, 60);
                    obj.UpdatedBy = ext;
                    obj.UpdatedDateTime = DateTime.Now;
                    _unitOfWork.Client.Update(obj);
                    TempData["success"] = "Client updated successfully";
                }
                _unitOfWork.Save();
                //TempData["success"] = "Day Off Requested successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult ChangeStatus(int? id)
        {
            Client cli = _unitOfWork.Client.GetFirstOrDefault(d => d.Id == id);
            if (cli == null)
            {
                return NotFound();
            }
            _unitOfWork.Client.ChangeStatus(cli);
            _unitOfWork.Save();
            TempData["success"] = "Client removed successfully";
            return RedirectToAction("Index");

        }
    }
}

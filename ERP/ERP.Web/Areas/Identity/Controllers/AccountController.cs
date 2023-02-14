using ERP.Models.Models;
using ERP.Models.Models.VM;
using ERP.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ERP.Web.Areas.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnurl=null)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                //Creating roles
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Client"));
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
                await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
                await _roleManager.CreateAsync(new IdentityRole("HR"));

            }

            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listItems = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });

            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Client",
                Text = "Client"
            });

            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Employee",
                Text = "Employee"
            });

            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Supervisor",
                Text = "Superviros"
            });

            listItems.Add(new SelectListItem()
            {
                Value = "Manager",
                Text = "Manager"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "HR",
                Text = "HR"
            });


            ViewData["ReturnUrl"] = returnurl;
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                RoleList = listItems
            };
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.RoleSelected !=null && model.RoleSelected.Length>0 && model.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Client")
                    {
                        await _userManager.AddToRoleAsync(user, "Client");
                    }
                    else if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Employee")
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                    }
                    else if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "HR")
                    {
                        await _userManager.AddToRoleAsync(user, "HR");
                    }
                    else if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Manager")
                    {
                        await _userManager.AddToRoleAsync(user, "Manager");
                    }
                    else if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Supervisor")
                    {
                        await _userManager.AddToRoleAsync(user, "Supervisor");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                    //return RedirectToRoute(new { action = "Index", controller = "Home", area = "Public" });
                    return Redirect(returnurl);
                }
                AddErrors(result);
            }

            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listItems = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });

            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Client",
                Text = "Client"
            });

            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Employee",
                Text = "Employee"
            });

            listItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Supervisor",
                Text = "Superviros"
            });

            listItems.Add(new SelectListItem()
            {
                Value = "Manager",
                Text = "Manager"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "HR",
                Text = "HR"
            });

            model.RoleList = listItems;

            return View(model);
        }


        [HttpGet]
        public IActionResult Login(string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //return LocalRedirect(returnurl);
                    return RedirectToRoute(new { action = "Index", controller = "Home", area = "Public" });
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attemp");
                    return View(model);
                }
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            }           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            //return RedirectToAction(nameof(HomeController.Index), "Home");
            return RedirectToAction("Index", "Home", new { Area = "Public" });
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnurl = null)
        {
            //request a redirect to the external login provider
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnurl = returnurl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Sign in the user with this external login provider, id the user already has a login
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                //update any authentication tokens

                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnurl);
            }
            else
            {
                //if the user does not have account, the we eill ask the user to create an acount
                ViewData["ReturnUrl"] = returnurl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);
                return View("ExternalLogingConfirmation", new ExternalLoginConfirmationViewModel { Email = email, Name = name });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnurl = null)
        {
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //get the info about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnurl);
                    }
                }
                AddErrors(result);
            }
            ViewData["ReturnUrl"] = returnurl;
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }
    }
}

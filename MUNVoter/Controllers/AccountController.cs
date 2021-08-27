using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MUNVoter.Identity;
using MUNVoter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MUNVoter.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public AccountController()
        {
            userManager= new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            userManager.PasswordValidator = new PasswordValidator()
            {
                /*
                RequireDigit = true,
                RequireLowercase=true,
                RequireUppercase=true,
                */
                RequiredLength = 6,
                
            };

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager) {

                RequireUniqueEmail = true,
            };
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model,string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong Username or Password");
                }
                else
                {
                    // Cookie creation
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties()
                    {

                        IsPersistent = true

                    };
                    authManager.SignOut();
                    authManager.SignIn(authProperties, identity);
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);

                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.Username;
                user.Email = model.Email;

                var result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Login");
        }
    }
}
namespace QMS.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using QMS.Models;
    using QMS.Web.Models;
    using Models.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Services;
    using AutoMapper;

    public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private UsersServices users;

        public UsersController(UsersServices users)
        {
            this.users = users;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    RegisterDate = DateTime.UtcNow
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ActionResult Edit()
        {
            return View("Edit");
        }

        public ActionResult Update(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.users.Update(model.Id, model.UserName, model.FirstName, model.LastName, model.PhoneNumber, model.Email);
                return RedirectToAction("Details", user.Id);
            }

            return View("Edit", model);
        }

        public ActionResult Details(string id)
        {
            var user = this.users.GetById(id);
            var userFromModel = Mapper.Map<UserDetailsModel>(user);

            return View("Details", userFromModel);
        }
    }
}
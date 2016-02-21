namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models.Users;
    using QMS.Models;
    using QMS.Web.Models;
    using Services;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    [Authorize(Roles = "admin, admin-users")]
    public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private UsersServices users;
        private RolesServices roles;

        public UsersController(UsersServices users, RolesServices roles)
        {
            this.users = users;
            this.roles = roles;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = this.users.All()
                .ProjectTo<UserDetailsViewModel>()
                .OrderBy(u => u.UserName);

            return View("Index", users);
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

        public ActionResult Edit(string id)
        {
            var user = this.users.GetById(id);
            var userFromModel = Mapper.Map<UserEditViewModel>(user);
            var userRoles = user.Roles.Select(r => r.RoleId);
            var userMissingRolesSelectListItems = roles.All()
                .Where(r => !userRoles.Contains(r.Id))
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).ToList();

            var userRolesSelectListItems = user.Roles.Select(r => new SelectListItem
            {
                Text = this.roles.GetRoleNameById(r.RoleId),
                Value = r.RoleId
            })
            .ToList();

            ViewBag.RolesMissing = userMissingRolesSelectListItems;
            ViewBag.RolesAvailable = userRolesSelectListItems;

            return View("Edit", userFromModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.users.Update(model.Id, model.UserName, model.FirstName, model.LastName, model.PhoneNumber, model.Email);
                TempData["Success"] = "User updated successfully.";
                return Redirect($"~/admin/users/edit/{user.Id}");
            }

            return View("Edit", model);
        }

        public ActionResult SetPassword(string userId)
        {
            return PartialView("_SetPassword", new UserResetPasswordFromAdminViewModel { UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPassword(UserResetPasswordFromAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = this.users.GetById(model.UserId);
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                TempData["Success"] = "User password has been reset";

                return RedirectToAction("Edit", new { id = model.UserId });
            }

            return PartialView("_SetPassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser(string roleId, string userId)
        {
            if (ModelState.IsValid)
            {
                this.roles.AddRoleToUser(userId, roleId);
                TempData["Success"] = $"Role added to user!";
                return this.Edit(userId);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUserRole(string roleId, string userId)
        {
            if (ModelState.IsValid)
            {
                this.roles.RemoveUserRole(userId, roleId);
                TempData["Success"] = $"Role added to user!";
                return this.Edit(userId);
            }

            return RedirectToAction("Index");
        }
    }
}
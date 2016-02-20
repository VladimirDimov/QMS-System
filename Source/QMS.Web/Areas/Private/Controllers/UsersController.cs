namespace QMS.Web.Areas.Private.Controllers
{
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QMS.Models;
    using QMS.Services;
    using QMS.Web.Models.Users;
    using System;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class UsersController : Controller
    {
        private UsersServices users;

        public UsersController(UsersServices users)
        {
            this.users = users;
        }

        // GET: Private/Users
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update()
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.users.GetById(userId);
            var userViewModel = Mapper.Map<UserEditViewModel>(user);

            return this.View("Update", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.GetUserId() != model.Id)
                {
                    throw new HttpException(401, "You can update only your profile");
                }

                this.users.Update(model.Id, model.UserName, model.FirstName, model.LastName,
                    model.PhoneNumber, model.Email);

                TempData["Success"] = "User profile successfully updated";
            }

            return View("Update", model);
        }

        public ActionResult ResetPassword()
        {
            var userId = this.User.Identity.GetUserId();
            return PartialView("_SetPassword", new UserResetPasswordViewModel { UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(UserResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.users.GetById(model.UserId);
                var store = new UserStore<User>();
                var userManager = new UserManager<User>(store);
                var currentUserHash = user.PasswordHash;

                userManager.PasswordHasher.VerifyHashedPassword(currentUserHash, model.OldPassword);

                this.users.SetPassword(model.UserId, model.Password);

                TempData["Success"] = "User password has been reset";
                return RedirectToAction("Update", new { id = model.UserId });
            }

            return PartialView("_SetPassword", model);
        }
    }
}
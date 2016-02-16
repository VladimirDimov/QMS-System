using AutoMapper.QueryableExtensions;
using QMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Controllers
{
    public class MessagesController : Controller
    {
        private UsersServices users;

        public MessagesController(UsersServices users)
        {
            this.users = users;
        }
        // GET: Messages
        public ActionResult Index()
        {
            var users = this.users.All()
                .Select(u => new SelectListItem
                {
                    Text = u.UserName,
                    Value = u.Id
                })
                .ToList();
            ViewBag.Users = users;

            return View("Index");
        }
    }
}
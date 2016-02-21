using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using QMS.Services;
using QMS.Web.Hubs;
using QMS.Web.ViewModels.Messages;
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
        private MessagesServices messages;

        public MessagesController(UsersServices users, MessagesServices messages)
        {
            this.users = users;
            this.messages = messages;
        }
        // GET: Messages
        public ActionResult Index(string receiverId)
        {
            var users = this.users.All()
                .Select(u => new SelectListItem
                {
                    Text = u.UserName,
                    Value = u.Id
                })
                .ToList();
            ViewBag.Users = users;

            if (receiverId != null)
            {
                ViewBag.ReceiverId = receiverId;
            }

            return View("Index");
        }

        public void SendMessage(MessageCreateViewModel model)
        {
        }

        public ActionResult LoadChatHistory()
        {
            var userId = this.User.Identity.GetUserId();
            var messages = this.messages.LoadUserChatHistory(userId)
                .ProjectTo<MessageDetailsViewModel>()
                .OrderByDescending(m => m.CreatedOn)
                .ToList();

            return PartialView("_ChatHistory", messages);
        }

        public ActionResult HasNewMessages()
        {
            var user = this.users.GetById(this.User.Identity.GetUserId());
            if (user.HasNewMessages)
            {
                return PartialView("_NewMessagesNote");
            }

            return null;
        }

        public void SetUserToNoNewMessages()
        {
            this.users.SetUserToNoNewMessages(this.User.Identity.GetUserId());
        }
    }
}
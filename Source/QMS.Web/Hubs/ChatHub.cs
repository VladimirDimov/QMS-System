namespace QMS.Web.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Services;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using QMS.Models;
    using System.Linq;
    using Data;
    using System;

    public class ChatHub : Hub
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
        private MessagesServices messages;

        //public ChatHub()
        //    : this(new MessagesServices(new QmsData()), new UsersServices(new QmsData()))
        //{

        //}

        public ChatHub(MessagesServices messages)
        {
            this.messages = messages;
        }

        public void Send(string receiverId, string title, string content)
        {
            //var receiver = this.users.GetById(receiverId);
            var senderUsername = Context.User.Identity.Name;

            if (!this.IsValidInput(receiverId, title, content))
            {
                return;
            }

            var message = this.messages.Create(
                title, content,
                Context.User.Identity.GetUserId(),
                new List<string> { receiverId });

            if (dict.ContainsKey(receiverId))
            {
                Clients.Client(dict[receiverId]).broadcastMessage(message.Sender.UserName,
                    message.Title, message.Content, message.CreatedOn);
            }
            else
            {
                Clients.Client(Context.ConnectionId).broadcastMessage("Server", "User is not connected");
            }
        }

        private bool IsValidInput(string receiverId, string title, string content)
        {
            if (receiverId == null)
            {
                return false;
            }

            if (title == null || 1 > title.Length || title.Length > 50)
            {
                return false;
            }

            if (content == null || 1 > content.Length || content.Length > 200)
            {
                return false;
            }

            return true;
        }

        public override Task OnConnected()
        {
            var userId = Context.User.Identity.GetUserId();
            if (!dict.ContainsKey(userId))
            {
                dict.Add(userId, Context.ConnectionId);
            }
            else
            {
                dict[userId] = Context.ConnectionId;
            }
            return base.OnConnected();
        }
    }
}
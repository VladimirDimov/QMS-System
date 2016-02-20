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
        //TODO: Make dictionary concurrent (use: ConcurrentDictionary)
        private MessagesServices messages;

        public ChatHub(MessagesServices messages)
        {
            this.messages = messages;
        }

        public static int NumberOfUsersOnLine
        {
            get { return dict.Count; }
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

            // Broadcast message to receiver
            if (dict.ContainsKey(receiverId))
            {
                var receiverClient = Clients.Client(dict[receiverId]);
                receiverClient.broadcastMessage(Context.User.Identity.Name,
                message.Title, message.Content, message.CreatedOn);
            }

            // Broadcast notification to sender if receipent is not on line
            if (!dict.ContainsKey(receiverId))
            {
                Clients.Client(Context.ConnectionId).broadcastMessage("Server", "User is not connected");
            }
            // Broadcast message to sender
            Clients.Client(Context.ConnectionId).broadcastMessage(Context.User.Identity.Name, message.Title,
                message.Content, message.CreatedOn);
        }

        public override Task OnConnected()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                this.CacheUserContextToDictionary();
            }

            return base.OnConnected();
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

        private void CacheUserContextToDictionary()
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
        }
    }
}
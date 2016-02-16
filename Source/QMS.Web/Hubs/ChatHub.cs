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

    public class ChatHub : Hub
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
        private MessagesServices messages;
        private UsersServices users;

        //public ChatHub()
        //    : this(new MessagesServices(new QmsData()), new UsersServices(new QmsData()))
        //{
             
        //}

        public ChatHub(MessagesServices messages, UsersServices users)
        {
            this.messages = messages;
            this.users = users;
        }

        public void Send(string receiverId, string title, string content)
        {
            //var receiver = this.users.GetById(receiverId);
            var senderUsername = Context.User.Identity.Name;
            var receiver = this.users.GetById(receiverId);

            var message = this.messages.Create(
                title, content,
                Context.User.Identity.GetUserId(),
                new List<User> { receiver });

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
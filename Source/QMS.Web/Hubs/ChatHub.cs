namespace QMS.Web.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Services;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;

    public class ChatHub : Hub
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();

        public void Send(string receiverId, string message)
        {
            //var receiver = this.users.GetById(receiverId);
            var senderUsername = Context.User.Identity.Name;

            if (dict.ContainsKey(receiverId))
            {
                Clients.Client(dict[receiverId]).broadcastMessage(senderUsername, message);
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
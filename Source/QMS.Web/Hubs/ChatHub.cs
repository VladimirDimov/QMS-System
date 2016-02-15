namespace QMS.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            var senderUsername = Context.User.Identity.Name;
            Clients.All.broadcastMessage(senderUsername, message);
        }
    }
}
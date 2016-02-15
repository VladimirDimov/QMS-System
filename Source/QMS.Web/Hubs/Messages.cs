namespace QMS.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class Messages : Hub
    {
        public void Send(string message)
        {
            Clients.All.broadcastMessage(message);
        }
    }
}
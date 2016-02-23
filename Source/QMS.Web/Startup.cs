using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using QMS.Data;
using QMS.Services;
using QMS.Web.Hubs;

[assembly: OwinStartupAttribute(typeof(QMS.Web.Startup))]
namespace QMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            this.ConfigureSignalR(app);
        }

        private void ConfigureSignalR(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
                typeof(ChatHub),
                () => new ChatHub(new MessagesServices(new QmsData())));
            app.MapSignalR();
        }
    }
}

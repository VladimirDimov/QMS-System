using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using QMS.Web.Hubs;

[assembly: OwinStartupAttribute(typeof(QMS.Web.Startup))]
namespace QMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

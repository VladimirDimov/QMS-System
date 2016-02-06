using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QMS.Web.Startup))]
namespace QMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

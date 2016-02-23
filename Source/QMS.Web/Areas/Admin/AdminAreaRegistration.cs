using System.Web.Mvc;

namespace QMS.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "QMS.Web.Areas.Admin.Controllers" });

            context.MapRoute(
                "Admin_Manage_Users",
                "Admin/users/{id}/{action}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "QMS.Web.Areas.Admin.Controllers" });
        }
    }
}
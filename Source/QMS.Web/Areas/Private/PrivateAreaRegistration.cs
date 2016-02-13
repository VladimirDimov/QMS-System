using System.Web.Mvc;

namespace QMS.Web.Areas.Private
{
    public class PrivateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Private";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Private_default",
                "Private/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "CreateNewRecord",
                "Private/{controller}/manage/{id}/{action}",
                new { action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "Areas" }
            );
        }
    }
}
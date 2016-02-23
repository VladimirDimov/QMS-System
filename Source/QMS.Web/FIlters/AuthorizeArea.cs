namespace QMS.Web.FIlters
{
    using Microsoft.AspNet.Identity;
    using Ninject;
    using QMS.Services;
    using Services.Contracts;
    using System.Linq;
    using System.Web.Mvc;

    public class AuthorizeArea : ActionFilterAttribute
    {

        [Inject]
        public IUsersServices UsersServices { get; set; }

        public string[] RolesRequired { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userContext = filterContext.HttpContext.User;

            foreach (var role in this.RolesRequired)
            {
                if (userContext.IsInRole(role))
                {
                    Continue(filterContext);
                    return;
                }
            }

            var areaIdAsString = filterContext.RequestContext.RouteData.Values["id"].ToString();
            if (areaIdAsString == null) Continue(filterContext);
            var areaId = int.Parse(areaIdAsString);

            var user = this.UsersServices.GetById(filterContext.HttpContext.User.Identity.GetUserId());

            if (user.Areas.Any(a => a.Id == areaId))
            {
                Continue(filterContext);
                return;
            }

            filterContext.Result = new RedirectResult("~/Account/Login");
        }

        private void Continue(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}
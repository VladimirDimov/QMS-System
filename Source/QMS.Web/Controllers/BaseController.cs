namespace QMS.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                filterContext.ExceptionHandled = true;

                if (filterContext.Exception is HttpException)
                {
                    if (!this.ControllerContext.RouteData.Values.ContainsKey("error"))
                    {
                        this.ControllerContext.RouteData.Values.Add("error", filterContext.Exception);
                    }

                    var httpException = (HttpException)filterContext.Exception;

                    switch (httpException.GetHttpCode())
                    {
                        case 404:
                            filterContext.HttpContext.Response.StatusCode = 404;
                            filterContext.HttpContext.Response.StatusDescription = httpException.Message;
                            this.View("Errors/Error404", null).ExecuteResult(this.ControllerContext);
                            break;
                        case 500:
                            filterContext.HttpContext.Response.StatusCode = 500;
                            filterContext.HttpContext.Response.StatusDescription = httpException.Message;
                            this.View("Errors/Error500", null).ExecuteResult(this.ControllerContext);
                            break;
                        default:
                            filterContext.HttpContext.Response.StatusDescription = httpException.Message;
                            this.View("GenericError", null).ExecuteResult(this.ControllerContext);
                            break;
                    }
                }
            }

            base.OnException(filterContext);
        }
    }
}
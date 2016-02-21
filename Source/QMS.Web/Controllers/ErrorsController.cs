namespace QMS.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    public class ErrorsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error401()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }
    }
}
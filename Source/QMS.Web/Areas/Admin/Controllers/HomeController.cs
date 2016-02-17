namespace QMS.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
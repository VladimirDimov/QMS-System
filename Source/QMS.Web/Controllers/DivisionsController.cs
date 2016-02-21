namespace QMS.Web.Controllers
{
    using QMS.Services;
    using QMS.Web.ViewModels.Divisions;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using Services.Contracts;

    public class DivisionsController : Controller
    {
        private IDivisionsServices divisions;

        public DivisionsController(IDivisionsServices divisions)
        {
            this.divisions = divisions;
        }

        // GET: Divisions
        public ActionResult Index()
        {
            var divisions = this.divisions.GetAll()
                .OrderBy(d => d.Name)
                .ProjectTo<DivisionListViewModel>();

            return View("Index", divisions);
        }
    }
}
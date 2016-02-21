namespace QMS.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using QMS.Services;
    using Services.Contracts;
    using System.Linq;
    using System.Web.Mvc;

    public class ProceduresController : Controller
    {
        private IProceduresServices procedures;

        public ProceduresController(IProceduresServices procedures)
        {
            this.procedures = procedures;
        }

        public ActionResult Index()
        {
            var procedures = this.procedures.All()
                .ProjectTo<QMS.Web.ViewModels.Procedures.ProcedureListViewModel>()
                .OrderBy(p => p.Name);

            return View("Index", procedures);
        }
    }
}
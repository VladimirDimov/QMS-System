namespace QMS.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using QMS.Services;
    using System.Linq;
    using System.Web.Mvc;

    public class ProceduresController : Controller
    {
        private ProceduresServices procedures;

        public ProceduresController(ProceduresServices procedures)
        {
            this.procedures = procedures;
        }

        public ActionResult Index()
        {
            var procedures = this.procedures.All()
                .ProjectTo<QMS.Web.Models.Procedures.ProcedureListViewModel>()
                .OrderBy(p => p.Name);

            return View("Index", procedures);
        }
    }
}
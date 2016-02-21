namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QMS.Services;
    using QMS.Web.Models.Procedures;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize(Roles = "admin, admin-procedures")]
    public class ProceduresController : Controller
    {
        private ProceduresServices procedures;

        public ProceduresController(ProceduresServices procedures)
        {
            this.procedures = procedures;
        }

        // GET: Admin/Procedure
        public ActionResult Index()
        {
            var procedures = this.procedures.All()
                .ProjectTo<ProcedureListViewModel>()
                .ToList();

            return View("Index", procedures);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProcedureCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var procedure = this.procedures.Add(model.Name, model.Description);
                return RedirectToAction("Details", new { id = procedure.Id });
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var procedure = this.procedures.GetById(id);
            var procedureViewModel = Mapper.Map<ProcedureUpdateViewModel>(procedure);

            return View("Edit", procedureViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProcedureUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.procedures.Update(model.Id, model.Name, model.Description);
                TempData["Success"] = "Procedure successfully updated";
            }

            return View("Edit", model);
        }

        public ActionResult Details(int id)
        {
            var procedure = this.procedures.GetById(id);
            var fromModel = Mapper.Map<ProcedureDetailsViewModel>(procedure);
            return View(fromModel);
        }
    }
}
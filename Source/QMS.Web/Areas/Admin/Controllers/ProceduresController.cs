using AutoMapper;
using AutoMapper.QueryableExtensions;
using QMS.Services;
using QMS.Web.Models.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Areas.Admin.Controllers
{
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
                .ProjectTo<ProcedureListModel>()
                .ToList();

            return View("Index", procedures);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProcedureCreateModel model)
        {
            if (ModelState.IsValid)
            {
                this.procedures.Add(model.Name, model.Description);
            }

            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProcedureUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                this.procedures.Update(model.Id, model.Name, model.Description);
            }

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var procedure = this.procedures.GetById(id);
            var fromModel = Mapper.Map<ProcedureDetailsModel>(procedure);
            return View(fromModel);
        }
    }
}
namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Qms.Common;
    using QMS.Models;
    using QMS.Web.ViewModels.Divisions;
    using Services.Contracts;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize(Roles = "admin, admin-divisions")]
    public class DivisionsController : Controller
    {
        private IDivisionsServices divisions;

        public DivisionsController(IDivisionsServices divisions)
        {
            this.divisions = divisions;
        }

        // GET: Admin/Divisions
        public ActionResult Index()
        {
            var allDivisions = this.divisions.GetAll()
                .OrderBy(d => d.Name)
                .ProjectTo<DivisionListViewModel>();

            return View("Index", allDivisions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DivisionRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newDivision = this.divisions.Create(model.Name, model.Description);
                return RedirectToAction("Details", new { id = newDivision.Id });
            }

            TempData["Success"] = SuccessMessagesConstants.DivisionCreated;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var division = this.divisions
                .GetById(id);

            if (division != null)
            {
                var divisionFromModel = Mapper.Map<DivisionDetailsViewModel>(division);
                return View("Details", divisionFromModel);
            }
            else
            {
                TempData["Error"] = $"Invalid division id: {id}";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            var division = this.divisions
                .GetById(id);

            if (division != null)
            {
                return View("Edit", Mapper.Map(division, typeof(Division), typeof(DivisionUpdateViewModel)));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DivisionUpdateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.divisions.Update(model.Id, model.Name, model.Description);
                TempData["Success"] = SuccessMessagesConstants.DivisionUpdated;
                return RedirectToAction("Details", new { id = model.Id });
            }

            return View("Edit", model);
        }

        public ActionResult Delete(int id)
        {
            this.divisions.delete(id);

            TempData["Success"] = SuccessMessagesConstants.DivisionDeleted;
            return RedirectToAction("Index");
        }
    }
}
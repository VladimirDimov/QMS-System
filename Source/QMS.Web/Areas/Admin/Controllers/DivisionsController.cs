namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QMS.Models;
    using QMS.Services;
    using QMS.Web.Models.Divisions;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize(Roles = "admin, admin-divisions")]
    public class DivisionsController : Controller
    {
        private DivisionsServices divisions;

        public DivisionsController(DivisionsServices divisions)
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
            try
            {
                this.divisions.Update(model.Id, model.Name, model.Description);
                TempData["Success"] = "Division successfully updated!";
                return RedirectToAction("Details", new { id = model.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            this.divisions.delete(id);
            TempData["Success"] = $"Division successfully deleted.";
            return RedirectToAction("Index");
        }
    }
}
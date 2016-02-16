using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Ajax.Utilities;
using QMS.Models;
using QMS.Services;
using QMS.Web.Models.Divisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
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
                .ProjectTo<DivisionsListResponseModel>()
                .ToList();

            return View("Index", allDivisions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DivisionRequestModel model)
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
                var divisionFromModel = Mapper.Map<DivisionsDetailsResponseModel>(division);
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
                return View("Edit", Mapper.Map(division, typeof(Division), typeof(DivisionsUpdateModel)));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DivisionsUpdateModel model)
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
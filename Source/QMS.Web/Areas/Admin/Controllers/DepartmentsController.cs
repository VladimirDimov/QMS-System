namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QMS.Models;
    using QMS.Services;
    using QMS.Web.Models.Departments;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize(Roles = "admin, admin-departments")]
    public class DepartmentsController : Controller
    {
        private DepartmentsServices departments;
        private DivisionsServices divisions;

        public DepartmentsController(DepartmentsServices departments, DivisionsServices divisions)
        {
            this.departments = departments;
            this.divisions = divisions;
        }
        // GET: Admin/Departments
        public ActionResult Index()
        {
            var departments = this.departments.All()
                .ProjectTo<DepartmentListResponseModel>()
                .ToList();

            return View("Index", departments);
        }

        public ActionResult Create()
        {
            ViewBag.Divisions = this.divisions.GetAll()
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                })
                .ToList();

            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                this.departments.Create(model.Name, model.Description, model.DivisionId);
                TempData["Success"] = "Department successfully created!";
                return Redirect("~/admin/departments");
            }

            return Create();
        }

        public ActionResult Edit(int id)
        {
            var department = this.departments.GetById(id);

            if (department == null)
            {
                return this.HttpNotFound($"Invalid department id: {id}");
            }

            ViewBag.Divisions = this.GetDivisionsListItems();
            return View(Mapper.Map(department, typeof(Department), typeof(DepartmentCreateModel)));
        }

        private IEnumerable<SelectListItem> GetDivisionsListItems()
        {
            return this.divisions.GetAll()
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                });
        }

        public ActionResult Details(int id)
        {
            var model = this.departments.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound($"Invalid department id: {id}");
            }

            var fromModel = Mapper.Map(
                model,
                typeof(Department),
                typeof(DepartmentDetailsModel));

            return View("Details", fromModel);
        }

        public ActionResult Update(DepartmentUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.departments.Update(model.Id, model.Name, model.Description, model.DivisionId);
                    TempData["Success"] = "Department successfully updated";
                    return this.Details(model.Id);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return this.Index();
                }
            }

            ViewBag.Divisions = this.GetDivisionsListItems();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            this.departments.Delete(id);
            TempData["Success"] = "Department successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
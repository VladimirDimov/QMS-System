using QMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using QMS.Web.Models.Areas;
using AutoMapper.QueryableExtensions;
using QMS.Models;

namespace QMS.Web.Areas.Admin.Controllers
{
    public class AreasController : Controller
    {
        private AreasServices areas;
        private DepartmentsServices departments;
        private UsersServices users;

        public AreasController(
            AreasServices areas,
            DepartmentsServices departments,
            UsersServices users)
        {
            this.areas = areas;
            this.departments = departments;
            this.users = users;
        }

        // GET: Admin/Areas
        public ActionResult Index()
        {
            var allAreas = this.areas.all()
                .ProjectTo<AreaListModel>()
                .ToList();

            return View(allAreas);
        }

        public ActionResult Create()
        {
            ViewBag.Departments = this.departments.All()
                .ToList()
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                });

            ViewBag.Employees = this.users.All()
                .Select(e => new SelectListItem
                {
                    Text = e.UserName,
                    Value = e.Id
                });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaCreateModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.areas.Add(model.Name, model.Description, model.DepartmentId);
            }

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var area = this.areas.GetById(id);
            var fromModel = Mapper.Map(area, typeof(Area), typeof(AreaDetailsModel));
            return View("Details", fromModel);
        }

        public ActionResult Edit(int id)
        {
            var dbModel = this.areas.GetById(id);
            var fromModel = Mapper.Map<AreaEditModel>(dbModel);
            return View(fromModel);
        }

        public ActionResult Update(AreaEditModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.areas.Update(model.Id, model.Name, model.Description);
                TempData["Success"] = "Area successfully created!";
                return this.Details(model.Id);
            };

            return View(model);
        }
    }
}
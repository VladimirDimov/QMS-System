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
using QMS.Web.Models.Users;
using Microsoft.AspNet.Identity;

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
            ViewBag.Departments = GetDepartmentsSelecItemsData();
            ViewBag.Employees = GetUsersSelecItemsData();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaCreateModel model)
        {
            if (this.ModelState.IsValid)
            {
                var areaId = this.areas.Add(model.Name, model.Description, model.DepartmentId);
                return RedirectToAction("Details", new { id = areaId });
            }

            ViewBag.Departments = GetDepartmentsSelecItemsData();
            ViewBag.Employees = GetUsersSelecItemsData();
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
            var users = this.users.All()
                .Select(u => new SelectListItem
                {
                    Text = u.UserName,
                    Value = u.Id
                });

            ViewBag.Users = users;
            var dbModel = this.areas.GetById(id);
            var fromModel = Mapper.Map<AreaEditModel>(dbModel);
            return View(fromModel);
        }

        public ActionResult Update(AreaEditModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.areas.Update(model.Id, model.Name, model.Description, model.EmployeeId);
                TempData["Success"] = "Area successfully created!";
                return this.Details(model.Id);
            };

            return View(model);
        }

        private IEnumerable<SelectListItem> GetUsersSelecItemsData()
        {
            return this.users.All()
                .Select(e => new SelectListItem
                {
                    Text = e.UserName,
                    Value = e.Id
                })
                .ToList();
        }

        private IEnumerable<SelectListItem> GetDepartmentsSelecItemsData()
        {
            return this.departments.All()
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                })
                .ToList();
        }
    }
}
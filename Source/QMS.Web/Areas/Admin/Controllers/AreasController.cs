namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QMS.Models;
    using QMS.Services;
    using Services.Contracts;
    using QMS.Web.ViewModels.Areas;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Qms.Common;

    [Authorize(Roles = "admin, admin-areas")]
    public class AreasController : Controller
    {
        private IAreasServices areas;
        private IDepartmentsServices departments;
        private IUsersServices users;

        public AreasController(
            IAreasServices areas,
            IDepartmentsServices departments,
            IUsersServices users)
        {
            this.areas = areas;
            this.departments = departments;
            this.users = users;
        }

        // GET: Admin/Areas
        public ActionResult Index()
        {
            var allAreas = this.areas.all()
                .OrderBy(a => a.Name)
                .ProjectTo<AreaListViewModel>();

            return View("Index", allAreas);
        }

        public ActionResult Create()
        {
            ViewBag.Departments = GetDepartmentsSelecItemsData();
            ViewBag.Employees = GetUsersSelecItemsData();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var areaId = this.areas.Add(model.Name, model.Description, model.DepartmentId, model.EmployeeId);
                return RedirectToAction("Details", new { id = areaId });
            }

            ViewBag.Departments = GetDepartmentsSelecItemsData();
            ViewBag.Employees = GetUsersSelecItemsData();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var area = this.areas.GetById(id);
            var fromModel = Mapper.Map(area, typeof(Area), typeof(AreaDetailsViewModel));
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
            var area = this.areas.GetById(id);
            var areaViewModel = Mapper.Map<AreaEditViewModel>(area);
            return View(areaViewModel);
        }

        public ActionResult Update(AreaEditViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.areas.Update(model.Id, model.Name, model.Description, model.EmployeeId);
                TempData["Success"] = SuccessMessagesConstants.AreaCreated;
                return this.Details(model.Id);
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            this.areas.Delete(id);

            TempData["Success"] = SuccessMessagesConstants.AreaDeleted;
            return View("Index");
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
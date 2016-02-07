namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper.QueryableExtensions;
    using QMS.Services;
    using QMS.Web.Models.Departments;
    using System.Linq;
    using System.Web.Mvc;

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

            return View(departments);
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

            TempData["Success"] = "Test";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentCreateModel model)
        {
            this.departments.Create(model.Name, model.Description, model.DivisionId);
            return Redirect("~/admin/departments");

        }
    }
}
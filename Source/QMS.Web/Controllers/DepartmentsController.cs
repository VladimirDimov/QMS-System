namespace QMS.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using QMS.Services.Contracts;
    using QMS.Web.ViewModels.Departments;
    using System.Linq;
    using System.Web.Mvc;

    public class DepartmentsController : Controller
    {
        private IDepartmentsServices departments;

        public DepartmentsController(IDepartmentsServices departments)
        {
            this.departments = departments;
        }

        // GET: Departments
        public ActionResult Index(int? divisionId)
        {
            var departments = this.departments.All()
                .Where(d => divisionId == null ? true : d.DivisionId == divisionId)
                .OrderBy(d => d.Name)
                .ProjectTo<DepartmentListViewModel>();

            return View("Index", departments);
        }
    }
}
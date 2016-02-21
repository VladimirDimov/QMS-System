namespace QMS.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QMS.Services;
    using QMS.Web.ViewModels.Areas;
    using System.Linq;
    using System.Web.Mvc;

    public class CompanyAreasController : Controller
    {
        private AreasServices areas;

        public CompanyAreasController(AreasServices areas)
        {
            this.areas = areas;
        }

        // GET: Departments
        public ActionResult Index(int? departmentId)
        {
            var areas = this.areas.all()
                .Where(d => departmentId == null ? true : d.DepartmentId == departmentId)
                .OrderBy(d => d.Name)
                .ProjectTo<AreaListViewModel>();

            return View("Index", areas);
        }

        public ActionResult Details(int id)
        {
            var area = this.areas.GetById(id);
            var areaFromModel = Mapper.Map<AreaDetailsViewModel>(area);

            return View("Details", areaFromModel);
        }
    }
}
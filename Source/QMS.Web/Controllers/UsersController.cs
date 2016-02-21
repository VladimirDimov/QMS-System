namespace QMS.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QMS.Services.Contracts;
    using QMS.Web.ViewModels.Users;
    using System.Linq;
    using System.Web.Mvc;

    public class UsersController : Controller
    {
        private IUsersServices users;

        public UsersController(IUsersServices users)
        {
            this.users = users;
        }

        public ActionResult Index(int? divisionId, int? departmentId)
        {
            var users = this.users.All()
                .Where(u => divisionId == null ? true : u.Areas.Any(a => a.Department.DivisionId == divisionId))
                .Where(u => departmentId == null ? true : u.Areas.Any(a => a.DepartmentId == departmentId))
                .OrderBy(u => u.UserName)
                .ProjectTo<UserDetailsViewModel>();

            return View("Index", users);
        }

        public ActionResult Details(string id)
        {
            var user = this.users.GetById(id);
            var userFromModel = Mapper.Map<UserDetailsViewModel>(user);

            return View("Details", userFromModel);
        }
    }
}
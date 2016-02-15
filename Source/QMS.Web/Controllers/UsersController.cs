using AutoMapper;
using AutoMapper.QueryableExtensions;
using QMS.Services;
using QMS.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Controllers
{
    public class UsersController : Controller
    {
        private UsersServices users;

        public UsersController(UsersServices users)
        {
            this.users = users;
        }

        public ActionResult Index(int? divisionId, int? departmentId)
        {
            var users = this.users.All()
                .Where(u => divisionId == null ? true : u.Areas.Any(a => a.Department.DivisionId == divisionId))
                .Where(u => departmentId == null ? true : u.Areas.Any(a => a.DepartmentId == departmentId))
                .OrderBy(u => u.UserName)
                .ProjectTo<UserDetailsModel>();

            return View("Index", users);
        }

        public ActionResult Details(string id)
        {
            var user = this.users.GetById(id);
            var userFromModel = Mapper.Map<UserDetailsModel>(user);

            return View("Details", userFromModel);
        }
    }
}
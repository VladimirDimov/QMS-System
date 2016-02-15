using AutoMapper.QueryableExtensions;
using QMS.Services;
using QMS.Web.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private DepartmentsServices departments;

        public DepartmentsController(DepartmentsServices departments)
        {
            this.departments = departments;
        }

        // GET: Departments
        public ActionResult Index(int? divisionId)
        {
            var departments = this.departments.All()
                .Where(d => divisionId == null ? true : d.DivisionId == divisionId)
                .OrderBy(d => d.Name)
                .ProjectTo<DepartmentListResponseModel>();

            return View("Index", departments);
        }
    }
}
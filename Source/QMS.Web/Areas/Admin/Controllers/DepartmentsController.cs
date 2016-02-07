using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Areas.Admin.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Admin/Departments
        public ActionResult Index()
        {
            return View();
        }
    }
}
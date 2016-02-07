using QMS.Services;
using QMS.Web.Models.Divisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Areas.Admin.Controllers
{
    public class DivisionsController : Controller
    {
        private DivisionsServices divisions;

        public DivisionsController(DivisionsServices divisions)
        {
            this.divisions = divisions;
        }

        // GET: Admin/Divisions
        public ActionResult Index()
        {
            var allDivisions = this.divisions.GetAll().ToList();
            return View("Index", allDivisions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DivisionRequestModel model)
        {
            var a = model;
            return View();
        }
    }
}
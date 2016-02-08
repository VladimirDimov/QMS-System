using QMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using QMS.Web.Models.Areas;
using AutoMapper.QueryableExtensions;

namespace QMS.Web.Areas.Admin.Controllers
{
    public class AreasController : Controller
    {
        private AreasServices areas;

        public AreasController(AreasServices areas)
        {
            this.areas = areas;
        }

        // GET: Admin/Areas
        public ActionResult Index()
        {
            var allAreas = this.areas.all()
                .ProjectTo<AreaListModel>()
                .ToList();

            return View(allAreas);
        }
    }
}
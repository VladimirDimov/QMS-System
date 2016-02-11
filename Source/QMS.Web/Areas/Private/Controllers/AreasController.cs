using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using QMS.Services;
using QMS.Web.Models.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AutoMapper;
using QMS.Web.Models.Records;

namespace QMS.Web.Areas.Private.Controllers
{
    public class AreasController : Controller
    {
        private AreasServices areas;
        private RecordsServices records;

        public AreasController(AreasServices areas, RecordsServices records)
        {
            this.areas = areas;
            this.records = records;
        }
        // GET: Private/Areas
        public ActionResult Index(int page = 1)
        {
            var areas = this.areas.all()
                .ProjectTo<AreaListModel>()
                .OrderBy(a => a.Name);

            return View(areas);
        }

        public ActionResult MyAreas()
        {
            var userId = this.User.Identity.GetUserId();
            var myAreas = this.areas.GetByUserId(userId)
                    .ProjectTo<AreaListModel>()
                    .OrderBy(a => a.Name);

            return View("Index", myAreas);
        }

        public ActionResult GetCurrentUserAreas()
        {
            var userId = this.User.Identity.GetUserId();
            var currentUserAreas = this.areas.GetByUserId(userId)
                    .ProjectTo<AreaShortModel>()
                    .OrderBy(a => a.Name)
                    .ToList();

            return PartialView("_UserAreasDropDownList", currentUserAreas);
        }

        public ActionResult Manage(int id)
        {
            var area = this.areas.GetById(id);
            var areaViewModel = Mapper.Map<AreaDetailsModel>(area);
            var recordsViewModel = this.records.GetByAreaId(id)
                .ProjectTo<RecordListModel>()
                .OrderBy(r => r.DateCreated);

            var areaManageViewModel = new AreaManageModel
            {
                Area = areaViewModel,
                Records = recordsViewModel
            };

            return View("Manage", areaManageViewModel);
        }
    }
}
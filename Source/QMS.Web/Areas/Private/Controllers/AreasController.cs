﻿namespace QMS.Web.Areas.Private.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FIlters;
    using Microsoft.AspNet.Identity;
    using QMS.Models;
    using QMS.Web.ViewModels.Areas;
    using QMS.Web.ViewModels.Records;
    using Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Timesheet;

    [Authorize]
    public class AreasController : Controller
    {
        private IAreasServices areas;
        private IRecordsServices records;
        private IDocumentsServices documents;
        private IUsersServices users;

        public AreasController(
            IAreasServices areas,
            IRecordsServices records,
            IDocumentsServices documents,
            IUsersServices users)
        {
            this.areas = areas;
            this.records = records;
            this.documents = documents;
            this.users = users;
        }

        public ActionResult Index(int page = 1)
        {
            var areas = this.areas.all()
                .ProjectTo<AreaListViewModel>()
                .OrderBy(a => a.Name);

            return View("Index", areas);
        }

        public ActionResult MyAreas()
        {
            var userId = this.User.Identity.GetUserId();
            var myAreas = this.areas.GetByUserId(userId)
                    .ProjectTo<AreaListViewModel>()
                    .OrderBy(a => a.Name);

            return View("Index", myAreas);
        }

        public ActionResult GetCurrentUserAreas()
        {
            var userId = this.User.Identity.GetUserId();
            var currentUserAreas = this.areas.GetByUserId(userId)
                    .ProjectTo<AreaShortViewModel>()
                    .OrderBy(a => a.Name)
                    .ToList();

            return PartialView("_UserAreasDropDownList", currentUserAreas);
        }

        [AuthorizeArea(RolesRequired = new string[] { "admin", "manage-all-areas" })]
        public ActionResult Manage(int id)
        {
            var records = this.records.GetByAreaId(id)
                .ProjectTo<RecordListViewModel>()
                .OrderBy(r => r.DateCreated);

            return this.ShowRecords(id, records);
        }

        [AuthorizeArea(RolesRequired = new string[] { "admin", "manage-all-areas" })]
        public ActionResult GetMissedRecords(int id)
        {
            var records = this.records.GetMissedByAreaId(id)
                .ProjectTo<RecordListViewModel>()
                .OrderBy(r => r.DateCreated);

            return this.ShowRecords(id, records);
        }

        [AuthorizeArea(RolesRequired = new string[] { "admin", "manage-all-areas" })]
        public ActionResult GetUpcomingRecords(int id)
        {
            var records = this.records.GetUpcomingByAreaId(id)
                .ProjectTo<RecordListViewModel>()
                .OrderBy(r => r.DateCreated);

            return this.ShowRecords(id, records);
        }

        [AuthorizeArea(RolesRequired = new string[] { "admin", "manage-all-areas" })]
        public ActionResult CreateRecord()
        {
            ViewBag.Documents = this.GetDocumentsSelectListItems();
            return View("CreateRecord");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeArea(RolesRequired = new string[] { "admin", "manage-all-areas" })]
        public ActionResult CreateRecord(int id, RecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var documentTemplateFilePath = Server.MapPath(this.documents.GetById(model.DocumentId).FilePath);
                var fileExtension = System.IO.Path.GetExtension(documentTemplateFilePath);
                var newRecord = this.records.Create(model.Title, model.Description, model.DateCreated, model.FinishingDate, model.Status, model.StatusDate, model.DocumentId, id, fileExtension);

                this.CreateFileOfRecord(this.documents.GetById(model.DocumentId).FilePath, newRecord.RecordFiles.ToList()[newRecord.RecordFiles.ToList().Count - 1].Path);

                return RedirectToAction("Manage", new { id = id });
            }

            ViewBag.Documents = this.GetDocumentsSelectListItems();
            return View(model);
        }

        /// <summary>
        /// Materializes the file. Copy from templates to existing file path property
        /// </summary>
        private void CreateFileOfRecord(string fromPath, string toPath)
        {
            var fromPathMapped = Server.MapPath(fromPath);
            var toPathMapped = Server.MapPath(toPath);
            var toPathDirectory = System.IO.Path.GetDirectoryName(toPathMapped);

            if (!System.IO.Directory.Exists(toPathDirectory))
            {
                System.IO.Directory.CreateDirectory(toPathDirectory);
            }

            System.IO.File.Copy(fromPathMapped, toPathMapped, true);
        }

        private IEnumerable<SelectListItem> GetDocumentsSelectListItems()
        {
            return this.documents.All()
                .Select(d => new SelectListItem
                {
                    Text = d.Title,
                    Value = d.Id.ToString()
                });
        }

        public ActionResult Timesheet(int id)
        {
            var activeRecords = this.areas.GetById(id).Records
                .Where(r => r.Status == RecordStatus.Active);

            var timesheetViewModel = new TimesheetViewModel
            {
                MinYear = activeRecords.Min(a => a.DateCreated).Year,
                MaxYear = activeRecords.Max(a => a.FinishingDate).Value.Year,
                Events = activeRecords
            };

            return View("Timesheet", timesheetViewModel);
        }

        private ActionResult ShowRecords(int id, IOrderedQueryable<RecordListViewModel> recordsListViewModel)
        {
            var area = this.areas.GetById(id);
            var areaViewModel = Mapper.Map<AreaDetailsViewModel>(area);
            var areaManageViewModel = new AreaManageViewModel
            {
                Area = areaViewModel,
                Records = recordsListViewModel
            };

            return View("Manage", areaManageViewModel);
        }
    }
}
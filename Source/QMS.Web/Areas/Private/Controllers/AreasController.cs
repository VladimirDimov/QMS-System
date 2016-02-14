namespace QMS.Web.Areas.Private.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using QMS.Models;
    using QMS.Services;
    using QMS.Web.Models.Areas;
    using QMS.Web.Models.Records;
    using Models.Timesheet;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class AreasController : Controller
    {
        private AreasServices areas;
        private RecordsServices records;
        private DocumentsServices documents;

        public AreasController(AreasServices areas, RecordsServices records, DocumentsServices documents)
        {
            this.areas = areas;
            this.records = records;
            this.documents = documents;
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

        //public ActionResult Upcoming()
        //{
        //    var userId = this.User.Identity.GetUserId();
        //    var myAreas = this.areas.GetByUserId(userId)
        //            .ProjectTo<AreaListModel>()
        //            .OrderBy(a => a.Name);

        //    return View("Index", myAreas);
        //}

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
            var recordsViewModel = this.records.GetByAreaId(id)
                .ProjectTo<RecordListModel>()
                .OrderBy(r => r.DateCreated);

            return this.LoadManagePage(id, recordsViewModel);
        }

        public ActionResult Missed(int id)
        {
            var recordsViewModel = this.records.GetPassedByAreaId(id)
                .ProjectTo<RecordListModel>()
                .OrderBy(r => r.DateCreated);

            return this.LoadManagePage(id, recordsViewModel);
        }

        public ActionResult Upcoming(int id)
        {
            var recordsViewModel = this.records.GetUpcomingByAreaId(id)
                .ProjectTo<RecordListModel>()
                .OrderBy(r => r.DateCreated);

            return this.LoadManagePage(id, recordsViewModel);
        }

        public ActionResult CreateRecord()
        {
            ViewBag.Documents = this.GetDocumentsSelectListItems();
            return View("CreateRecord");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRecord(int id, RecordCreateModel model)
        {

            if (ModelState.IsValid)
            {
                var documentTemplateFilePath = Server.MapPath(this.documents.GetById(model.DocumentId).FilePath);
                var fileExtension = System.IO.Path.GetExtension(documentTemplateFilePath);
                var newRecord = this.records.Create(model.Title, model.Description, model.DateCreated, model.FinishingDate,
                model.Status, model.StatusDate, model.DocumentId, id, fileExtension);

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

        private ActionResult LoadManagePage(int id, IOrderedQueryable<RecordListModel> recordsViewModel)
        {
            var area = this.areas.GetById(id);
            var areaViewModel = Mapper.Map<AreaDetailsModel>(area);

            var areaManageViewModel = new AreaManageModel
            {
                Area = areaViewModel,
                Records = recordsViewModel
            };

            return View("Manage", areaManageViewModel);
        }
    }
}
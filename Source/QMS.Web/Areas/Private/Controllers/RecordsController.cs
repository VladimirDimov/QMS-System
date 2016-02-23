namespace QMS.Web.Areas.Private.Controllers
{
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Qms.Common;
    using QMS.Web.ViewModels.Records;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class RecordsController : Controller
    {
        private IRecordsServices records;
        private IDocumentsServices documents;
        private IAreasServices areas;
        private IRecordFilesServices recordFiles;

        public RecordsController(
            IRecordsServices records,
            IDocumentsServices documents,
            IAreasServices areas,
            IRecordFilesServices recordFiles)
        {
            this.records = records;
            this.documents = documents;
            this.areas = areas;
            this.recordFiles = recordFiles;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public FileResult GetRecordFile(int id)
        {
            var record = this.records.GetById(id);
            var filePath = record.RecordFiles.ToList().LastOrDefault().Path;
            var filePathMapped = Server.MapPath(filePath);
            var bytes = System.IO.File.ReadAllBytes(filePathMapped);
            var extension = System.IO.Path.GetExtension(filePathMapped);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(filePathMapped);
            return File(bytes, extension, $"{ fileName}-{DateTime.UtcNow.ToShortDateString()}{extension}");
        }

        public ActionResult Edit(int id)
        {
            var record = this.records.GetById(id);
            var area = this.areas.GetById(record.AreaId);
            if (User.Identity.GetUserId() != area.EmployeeId)
            {
                return new HttpUnauthorizedResult(ErrorMessagesConstants.NotAuthorizedForThisAction);
            }

            var recordFromModel = Mapper.Map<RecordUpdateViewModel>(record);

            ViewBag.Documents = this.GetDocumentsSelectListItems();
            return View("Edit", recordFromModel);
        }

        public ActionResult Update(RecordUpdateViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var record = this.records.Update(model.Id, model.Title, model.Description, model.Status,
                model.StatusDate, model.DateCreated, model.FinishingDate, model.DocumentId);

                if (file != null)
                {
                    var recordFile = this.recordFiles.Create(model.Id);
                    var extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    var savePath = $"~/Files/Records/{recordFile.Id % 1000}/{model.Id}-{DateTime.UtcNow.ToShortDateString()}{extension}";
                    var saveDirPathMapped = System.IO.Path.GetDirectoryName(Server.MapPath(savePath));

                    if (!System.IO.Directory.Exists(saveDirPathMapped))
                    {
                        System.IO.Directory.CreateDirectory(saveDirPathMapped);
                    }

                    file.SaveAs(Server.MapPath(savePath));
                    this.recordFiles.SetPath(recordFile, savePath);

                    this.records.UpdateFile(model.Id, recordFile.Id);
                }

                TempData["Success"] = $"Record with id {record.Id} successfully updated.";
                return RedirectToAction("Manage", "Areas", new { Area = "Private", id = record.AreaId });
            }

            return View(model);
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

        public ActionResult DeleteRecord(int id)
        {
            var record = records.GetById(id);
            if (User.Identity.GetUserId() != record.Area.EmployeeId)
            {
                return new HttpUnauthorizedResult(ErrorMessagesConstants.NotAuthorizedForThisAction);
            }

            this.records.Delete(id);
            return RedirectToAction("Manage", "Areas", new { id = record.AreaId });
        }
    }
}
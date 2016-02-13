namespace QMS.Web.Areas.Private.Controllers
{
    using QMS.Services;
    using QMS.Web.Models.Records;
    using System.Web.Mvc;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using System.Web;

    public class RecordsController : Controller
    {
        private RecordsServices records;
        private DocumentsServices documents;
        private AreasServices areas;

        public RecordsController(RecordsServices records, DocumentsServices documents, AreasServices areas)
        {
            this.records = records;
            this.documents = documents;
            this.areas = areas;
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
                return new HttpUnauthorizedResult("You are not authorized for this action.");
            }

            var recordFromModel = Mapper.Map<RecordUpdateModel>(record);

            ViewBag.Documents = this.GetDocumentsSelectListItems();
            return View("Edit", recordFromModel);
        }

        public ActionResult Update(RecordUpdateModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var record = this.records.Update(model.Id, model.Title, model.Description, model.Status,
                model.StatusDate, model.DateCreated, model.FinishingDate, model.DocumentId);

                if (file != null)
                {
                    //TODO: Add new FileRecordsServices and add the logic of creating new record file there.
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
                return new HttpUnauthorizedResult("You are not authorized for this action.");
            }

            this.records.Delete(id);
            return RedirectToAction("Manage", "Areas", new { id = record.AreaId });
        }
    }
}
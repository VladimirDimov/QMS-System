namespace QMS.Web.Areas.Private.Controllers
{
    using QMS.Services;
    using QMS.Web.Models.Records;
    using System.Web.Mvc;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RecordsController : Controller
    {
        private RecordsServices records;
        private DocumentsServices documents;

        public RecordsController(RecordsServices records, DocumentsServices documents)
        {
            this.records = records;
            this.documents = documents;
        }

        public ActionResult Index()
        {
            return View();
        }

        public FileResult GetRecordFile(int id)
        {
            var record = this.records.GetById(id);
            var filePath = record.RecordFile.Path;
            var filePathMapped = Server.MapPath(filePath);
            var bytes = System.IO.File.ReadAllBytes(filePathMapped);
            var extension = System.IO.Path.GetExtension(filePathMapped);
            var fileName = System.IO.Path.GetFileName(filePathMapped);
            return File(bytes, extension, $"{ fileName}-{DateTime.UtcNow.ToShortDateString()}");
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
    }
}
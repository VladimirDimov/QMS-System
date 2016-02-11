namespace QMS.Web.Areas.Private.Controllers
{
    using QMS.Services;
    using QMS.Web.Models.Records;
    using System.Web.Mvc;

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

        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult Create(RecordCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var documentTemplateFilePath = Server.MapPath(this.documents.GetById(model.DocumentId).FilePath);
                var newRecord = this.records.Create(model.Title, model.Description, model.DateCreated, model.FinishingDate,
                model.Status, model.StatusDate, model.DocumentId, model.AreaId);

                this.CreateFileOfRecord(this.documents.GetById(model.DocumentId).FilePath, newRecord.RecordFile.Path);
                return View();
            }

            return View(model);
        }

        /// <summary>
        /// Materializes the file. Copy from templates to existing file path property
        /// </summary>
        private void CreateFileOfRecord(string fromPath, string toPath)
        {
            System.IO.File.Copy(Server.MapPath(fromPath), Server.MapPath(toPath));
        }
    }
}
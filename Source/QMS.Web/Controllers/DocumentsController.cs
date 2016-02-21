namespace QMS.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using QMS.Services;
    using Services.Contracts;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class DocumentsController : Controller
    {
        private IDocumentsServices documents;

        public DocumentsController(IDocumentsServices documents)
        {
            this.documents = documents;
        }

        public ActionResult Index(int? procedureId)
        {
            var documents = this.documents.All()
                .Where(p => procedureId == null ? true : p.ProcedureId == procedureId)
                .ProjectTo<QMS.Web.ViewModels.Documents.DocumentListViewModel>()
                .OrderBy(d => d.Title);

            return View("Index", documents);
        }

        public FileResult GetFile(int documentId)
        {
            var document = this.documents.GetById(documentId);
            var documentPathMapped = Server.MapPath(document.FilePath);

            var fileName = System.IO.Path.GetFileNameWithoutExtension(documentPathMapped);
            var extension = System.IO.Path.GetExtension(documentPathMapped);
            byte[] fileBytes = System.IO.File.ReadAllBytes(documentPathMapped);
            var dateTimeFormatted = DateTime.UtcNow.ToString().Replace(':', '_');

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, $"{fileName}-{dateTimeFormatted}{extension}");
        }
    }
}
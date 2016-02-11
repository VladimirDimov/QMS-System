
namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QMS.Helpers;
    using QMS.Services;
    using QMS.Web.Models.Documents;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.IO;
    using Models.Procedures;

    public class DocumentsController : Controller
    {
        private DocumentsServices documents;
        private ProceduresServices procedures;

        public DocumentsController(DocumentsServices documents, ProceduresServices procedures)
        {
            this.documents = documents;
            this.procedures = procedures;
        }

        public ActionResult Index()
        {
            var allDocumentsModel = this.documents
                .All()
                .ProjectTo<DocumentListModel>()
                .ToList();

            return View(allDocumentsModel);
        }

        public ActionResult Details(int id)
        {
            var dbModel = this.documents.GetById(id);

            if (dbModel == null)
            {
                return this.HttpNotFound($"Invalid document id: {id}");
            }

            var fromModel = Mapper.Map<DocumentDetailsModel>(dbModel);
            return View("Details", fromModel);
        }

        public ActionResult Create()
        {
            ViewBag.Procedures = GetProceduresListItems();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentCreateModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                var newDocumentId = this.documents
                    .Add(model.Title, model.Description, model.Code, model.ProcedureId);

                this.SaveDocumentFile(newDocumentId, file);

                TempData["Success"] = "Document successfully created";
                return RedirectToAction("Details", new { id = newDocumentId });
            }

            ViewBag.Procedures = GetProceduresListItems();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var dbModel = this.documents.GetById(id);

            if (dbModel == null)
            {
                return this.HttpNotFound($"Invalid document id: {id}");
            }

            ViewBag.Procedures = GetProceduresListItems();
            var fromModel = Mapper.Map<DocumentUpdateModel>(dbModel);
            return View(fromModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DocumentUpdateModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                this.documents.Update(
                    model.Id,
                    model.Title,
                    model.Description,
                    model.Code,
                    model.ProcedureId);

                if (file != null)
                {
                    this.SaveDocumentFile(model.Id, file);
                }

                TempData["Success"] = "Document successfully updated";
                return RedirectToAction("Details", new { id = model.Id });
            }

            return View(model);
        }

        public FileResult GetFile(int id)
        {
            var document = this.documents.GetById(id);
            var path = Server.MapPath(document.FilePath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = System.IO.Path.GetFileName(path);
            string extension = System.IO.Path.GetExtension(path);
            return File(fileBytes, extension, fileName);
        }

        private void SaveDocumentFile(int documentId, HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("Cannot save null file.");
            }

            var documentModel = this.documents.GetById(documentId);

            if (documentModel.FilePath != null)
            {
                System.IO.File.Delete(Server.MapPath(documentModel.FilePath));
            }

            var savingPath = this.GetDocumentSavingPath(file, documentId);
            file.SaveAs(Server.MapPath(savingPath));
            this.documents.UpdateFilePath(documentModel, savingPath);
        }

        private string GetDocumentSavingPath(HttpPostedFileBase file, int documentId)
        {
            if (file == null)
            {
                throw new ArgumentNullException("Cannot save null file.");
            }

            var savingDirName = documentId % 100;

            var filesHelper = new FilesHelper();
            var fileExtension = filesHelper.GetFileExtension(file.FileName);
            var saveDirPath = $"~/Files/Documents/{savingDirName}";

            if (!Directory.Exists(saveDirPath))
            {
                Directory.CreateDirectory(Server.MapPath(saveDirPath));
            }

            return $"{saveDirPath}/{documentId}{fileExtension}";
        }

        private IEnumerable<SelectListItem> GetProceduresListItems()
        {
            var allProcedures = this.procedures.All()
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToList();

            return allProcedures;
        }
    }
}
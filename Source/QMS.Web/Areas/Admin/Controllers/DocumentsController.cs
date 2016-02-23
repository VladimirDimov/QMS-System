﻿
namespace QMS.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Qms.Common;
    using QMS.Helpers;
    using QMS.Web.ViewModels.Documents;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize(Roles = "admin, admin-documents")]
    public class DocumentsController : Controller
    {
        private IDocumentsServices documents;
        private IProceduresServices procedures;

        public DocumentsController(IDocumentsServices documents, IProceduresServices procedures)
        {
            this.documents = documents;
            this.procedures = procedures;
        }

        public ActionResult Index()
        {
            var allDocumentsModel = this.documents.All()
                .OrderBy(d => d.LastUpdate)
                .ProjectTo<DocumentListViewModel>();

            return View(allDocumentsModel);
        }

        public ActionResult Details(int id)
        {
            var dbModel = this.documents.GetById(id);

            if (dbModel == null)
            {
                return this.HttpNotFound($"Invalid document id: {id}");
            }

            var fromModel = Mapper.Map<DocumentDetailsViewModel>(dbModel);
            return View("Details", fromModel);
        }

        public ActionResult Create()
        {
            ViewBag.Procedures = GetProceduresListItems();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentCreateViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                var newDocumentId = this.documents
                    .Add(model.Title, model.Description, model.Code, model.ProcedureId);

                this.SaveDocumentFile(newDocumentId, file);

                TempData["Success"] = SuccessMessagesConstants.DocumentCreated;
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
            var fromModel = Mapper.Map<DocumentUpdateViewModel>(dbModel);
            return View(fromModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DocumentUpdateViewModel model, HttpPostedFileBase file)
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

                TempData["Success"] = SuccessMessagesConstants.DocumentUpdated;
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
                throw new ArgumentNullException(ErrorMessagesConstants.CannotSaveNullFile);
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
                throw new ArgumentNullException(ErrorMessagesConstants.CannotSaveNullFile);
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
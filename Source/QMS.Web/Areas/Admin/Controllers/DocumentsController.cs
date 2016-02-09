using AutoMapper;
using AutoMapper.QueryableExtensions;
using QMS.Services;
using QMS.Web.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Areas.Admin.Controllers
{
    public class DocumentsController : Controller
    {
        private DocumentsServices documents;

        public DocumentsController(DocumentsServices documents)
        {
            this.documents = documents;
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

        public ActionResult Create(DocumentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var id = this.documents.Add(model.Title, model.Description, model.Code);
                TempData["Success"] = "Document successfully created";
                return RedirectToAction("Details", new { id = id });
            }

            return View(model);
        }

        public ActionResult Update(DocumentUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                this.documents.Update(model.Title, model.Description, model.Code);
                TempData["Success"] = "Document successfully created";
                return RedirectToAction("Details", new { id = model.Id });
            }

            return View(model);
        }
    }
}
using QMS.Services;
using QMS.Web.Models.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Areas.Private.Controllers
{
    public class NotesController : Controller
    {
        private NotesServices notes;

        public NotesController(NotesServices notes)
        {
            this.notes = notes;
        }
        // GET: Private/Notes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNote(int recordId)
        {
            ViewBag.RecordId = recordId;
            return PartialView("_AddNote");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNote(NoteCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.notes.Create(model.RecordId, model.Title, model.Text);
                return Redirect($"~/Private/Records/Edit/{model.RecordId}");
            }

            return RedirectToAction("Edit", "Records", new { id = model.RecordId });
        }

        [HttpPost]
        public ActionResult DeleteNote(int noteId, int recordId)
        {
            this.notes.delete(noteId);
            return RedirectToAction("Edit", "Records", new { id = recordId });
        }
    }
}
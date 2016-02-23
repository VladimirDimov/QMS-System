namespace QMS.Web.Areas.Private.Controllers
{
    using QMS.Web.ViewModels.Notes;
    using Services.Contracts;
    using System.Web.Mvc;

    [Authorize]
    public class NotesController : Controller
    {
        private INotesServices notes;

        public NotesController(INotesServices notes)
        {
            this.notes = notes;
        }

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
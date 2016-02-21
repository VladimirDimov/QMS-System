namespace QMS.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using QMS.Services;
    using QMS.Web.Hubs;
    using QMS.Web.Models.Documents;
    using QMS.Web.Models.Notes;
    using QMS.Web.Models.Records;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        private NotesServices notes;
        private RecordsServices records;
        private DocumentsServices documents;
        private UsersServices users;

        public HomeController(
            NotesServices notes,
            RecordsServices records,
            DocumentsServices documents,
            UsersServices users)
        {
            this.notes = notes;
            this.records = records;
            this.documents = documents;
            this.users = users;
        }

        public ActionResult Index()
        {
            this.ViewBag.NumberOfRegisteredUsers = this.users.All().Count();
            this.ViewBag.NumberOfUsersOnLine = ChatHub.NumberOfUsersOnLine;

            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Contact");
        }

        [ChildActionOnly]
        public ActionResult GetUserNotes()
        {
            var userId = this.User.Identity.GetUserId();
            var notes = this.notes.GetUserNotes(userId)
                .OrderByDescending(n => n.CreatedOn)
                .ProjectTo<NoteDetailsModel>();

            return this.PartialView("Home/_HomePageNotes", notes);
        }

        public ActionResult GetUserUpcomingRecords()
        {
            var userId = this.User.Identity.GetUserId();
            var upcomingRecords = this.records.GetUserUpcomingRecords(userId)
                .OrderBy(r => r.FinishingDate)
                .ProjectTo<RecordListModel>();

            return this.PartialView("Home/_HomePageUpcomingRecords", upcomingRecords);
        }

        [OutputCache(Duration = 60 * 60)]
        public ActionResult GetMostResentDocuments()
        {
            var mostRecentDocuments = this.documents.All()
                .ProjectTo<DocumentListViewModel>()
                .OrderBy(d => d.LastUpdate);

            return this.PartialView("Home/_HomePageMostRecentDocuments", mostRecentDocuments);
        }
    }
}
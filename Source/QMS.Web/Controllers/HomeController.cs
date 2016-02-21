namespace QMS.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using QMS.Services;
    using Services.Contracts;
    using QMS.Web.Hubs;
    using QMS.Web.ViewModels.Documents;
    using QMS.Web.ViewModels.Notes;
    using QMS.Web.ViewModels.Records;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        private INotesServices notes;
        private IRecordsServices records;
        private IDocumentsServices documents;
        private IUsersServices users;

        public HomeController(
            INotesServices notes,
            IRecordsServices records,
            IDocumentsServices documents,
            IUsersServices users)
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
                .ProjectTo<NoteDetailsViewModel>();

            return this.PartialView("Home/_HomePageNotes", notes);
        }

        public ActionResult GetUserUpcomingRecords()
        {
            var userId = this.User.Identity.GetUserId();
            var upcomingRecords = this.records.GetUserUpcomingRecords(userId)
                .OrderBy(r => r.FinishingDate)
                .ProjectTo<RecordListViewModel>();

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
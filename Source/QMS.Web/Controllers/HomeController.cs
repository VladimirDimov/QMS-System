using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using QMS.Services;
using QMS.Web.Models.Notes;
using QMS.Web.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private NotesServices notes;
        private RecordsServices records;

        public HomeController(NotesServices notes, RecordsServices records)
        {
            this.notes = notes;
            this.records = records;
        }

        public ActionResult Index()
        {
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
    }
}
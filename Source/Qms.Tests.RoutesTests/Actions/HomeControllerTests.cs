namespace Qms.Tests.RoutesTests.Actions
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using QMS.Models;
    using QMS.Services.Contracts;
    using QMS.Web.Controllers;
    using QMS.Web.ViewModels.Notes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using TestStack.FluentMVCTesting;

    class HomeControllerTests
    {
        private IQueryable<User> users = new List<User>
        {
            new User(),
            new User(),
            new User(),
            new User(),
        }
        .AsQueryable();

        private IQueryable<Note> notes = new List<Note>()
        {
            new Note(),
            new Note(),
            new Note(),
            new Note(),
        }
        .AsQueryable();

        [Test]
        public void IndexShouldReturnViewIndex()
        {
            var request = new Mock<HttpRequestBase>();
            var notesServices = new Mock<INotesServices>().Object;
            var recordsServices = new Mock<IRecordsServices>().Object;
            var documentsServices = new Mock<IDocumentsServices>().Object;
            var usersServices = new Mock<IUsersServices>().Object;

            request.SetupGet(x => x.IsAuthenticated).Returns(false);

            var homeController = new HomeController(notesServices, recordsServices,
                documentsServices, usersServices);

            ViewResult viewResult = homeController.Index() as ViewResult;

            Assert.AreEqual(viewResult.ViewName, "Index");
        }

        [Test]
        [ExpectedException(typeof(HttpException))]
        public void GetUserNotesShouldThrowIfUserIsNotAuthorized()
        {
            var notesServices = new Mock<INotesServices>().Object;
            var recordsServices = new Mock<IRecordsServices>().Object;
            var documentsServices = new Mock<IDocumentsServices>().Object;
            var usersServices = new Mock<IUsersServices>();

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(false);

            var context = new Mock<HttpContextBase>();
            context.Setup(x => x.Request).Returns(request.Object);

            var homeController = new HomeController(notesServices, recordsServices,
                documentsServices, usersServices.Object);

            homeController.ControllerContext = new ControllerContext(context.Object, new System.Web.Routing.RouteData(), homeController);

            homeController.GetUserNotes();
        }

        [Test]
        public void GetUserNotesShouldReturnProperResults()
        {
            var userId = Guid.NewGuid().ToString();
            var notesServices = new Mock<INotesServices>();
            notesServices.Setup(x => x.GetUserNotes(It.IsAny<string>())).Returns(notes);
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(true);

            var recordsServices = new Mock<IRecordsServices>().Object;
            var documentsServices = new Mock<IDocumentsServices>().Object;
            var usersServices = new Mock<IUsersServices>();

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("User");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            fakeHttpContext.Setup(t => t.Request).Returns(request.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var homeController = new HomeController(notesServices.Object, recordsServices,
                documentsServices, usersServices.Object)
            {
                ControllerContext = controllerContext.Object
            };

            Mapper.CreateMap<Note, NoteDetailsViewModel>();

            homeController.WithCallTo(c => c.GetUserNotes())
                .ShouldRenderPartialView("Home/_HomePageNotes")
                .WithModel<IQueryable<NoteDetailsViewModel>>(viewModel =>
                {
                    Assert.AreEqual(4, viewModel.Count());
                });
        }
    }
}

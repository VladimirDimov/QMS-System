namespace Qms.Tests.RoutesTests.Actions
{
    using AutoMapper;
    using Fakes;
    using Moq;
    using NUnit.Framework;
    using QMS.Models;
    using QMS.Services.Contracts;
    using QMS.Web.Areas.Private.Controllers;
    using QMS.Web.ViewModels.Areas;
    using QMS.Web.ViewModels.Records;
    using QMS.Web.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using TestStack.FluentMVCTesting;

    class PrivateAreasControllerTests
    {
        private IQueryable<Area> areas = new List<Area>
        {
            new Area(),
            new Area(),
            new Area(),
            new Area(),
        }
        .AsQueryable();

        IQueryable<Record> records = new List<Record>
        {
            new Record(),
            new Record(),
            new Record(),
            new Record(),
        }
        .AsQueryable();

        [Test]
        public void IndexShouldReturnProperResult()
        {
            var request = new Mock<HttpRequestBase>();

            var areasFake = new Mock<IAreasServices>();
            areasFake.Setup(a => a.all()).Returns(areas);

            var recordsFake = new Mock<IRecordsServices>().Object;
            var documentsFake = new Mock<IDocumentsServices>().Object;
            var usersFake = new Mock<IUsersServices>().Object;

            request.SetupGet(x => x.IsAuthenticated).Returns(false);

            var controller = new AreasController(areasFake.Object, recordsFake, documentsFake, usersFake);

            Mapper.CreateMap<Area, AreaListViewModel>();
            Mapper.CreateMap<User, UserShortViewModel>();

            ViewResult viewResult = controller.Index() as ViewResult;

            controller.WithCallTo(c => c.Index(1))
                .ShouldRenderView("Index")
                .WithModel<IQueryable<AreaListViewModel>>(x => Assert.AreEqual(4, x.Count()));
        }

        [Test]
        public void MyAreasShouldReturnProperResult()
        {
            var userId = Guid.NewGuid().ToString();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(true);

            var areasFake = new Mock<IAreasServices>();
            areasFake.Setup(a => a.GetByUserId(It.IsAny<string>())).Returns(areas);

            var recordsFake = new Mock<IRecordsServices>().Object;
            var documentsFake = new Mock<IDocumentsServices>().Object;
            var usersFake = new Mock<IUsersServices>().Object;

            request.SetupGet(x => x.IsAuthenticated).Returns(false);

            var principalFake = new Mock<IPrincipal>();
            principalFake.Setup(x => x.Identity).Returns(new IdentityFake());

            var fakeHttpContext = new Mock<HttpContextBase>();
            fakeHttpContext.Setup(x => x.User).Returns(principalFake.Object);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var controller = new AreasController(areasFake.Object, recordsFake, documentsFake, usersFake)
            {
                ControllerContext = controllerContext.Object
            };

            Mapper.CreateMap<Area, AreaListViewModel>();
            Mapper.CreateMap<User, UserShortViewModel>();

            ViewResult viewResult = controller.Index() as ViewResult;

            controller.WithCallTo(c => c.MyAreas())
                .ShouldRenderView("Index")
                .WithModel<IQueryable<AreaListViewModel>>(x => Assert.AreEqual(4, x.Count()));
        }

        [Test]
        public void GetCurrentUserAreasShouldReturnProperResult()
        {
            var userId = Guid.NewGuid().ToString();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(true);

            var areasFake = new Mock<IAreasServices>();
            areasFake.Setup(a => a.GetByUserId(It.IsAny<string>())).Returns(areas);

            var recordsFake = new Mock<IRecordsServices>().Object;
            var documentsFake = new Mock<IDocumentsServices>().Object;
            var usersFake = new Mock<IUsersServices>().Object;

            request.SetupGet(x => x.IsAuthenticated).Returns(false);

            var controller = new AreasController(areasFake.Object, recordsFake, documentsFake, usersFake)
            {
                ControllerContext = FakeControllerContext.GetFakeControllerContextWithFakeIdentity()
            };

            Mapper.CreateMap<Area, AreaShortViewModel>();
            Mapper.CreateMap<Area, AreaListViewModel>();
            Mapper.CreateMap<User, UserShortViewModel>();

            ViewResult viewResult = controller.Index() as ViewResult;

            controller.WithCallTo(c => c.GetCurrentUserAreas())
                .ShouldRenderPartialView("_UserAreasDropDownList")
                .WithModel<List<AreaShortViewModel>>(x => Assert.AreEqual(4, x.Count()));
        }

        [Test]
        public void ManageShouldReturnProperResult()
        {
            var userId = Guid.NewGuid().ToString();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated).Returns(true);

            var areasFake = new Mock<IAreasServices>();
            areasFake.Setup(a => a.GetByUserId(It.IsAny<string>())).Returns(areas);

            var recordsFake = new Mock<IRecordsServices>();
            recordsFake.Setup(x => x.GetByAreaId(It.IsAny<int>())).Returns(this.records);

            var documentsFake = new Mock<IDocumentsServices>().Object;
            var usersFake = new Mock<IUsersServices>().Object;

            request.SetupGet(x => x.IsAuthenticated).Returns(false);

            var controller = new AreasController(areasFake.Object, recordsFake.Object, documentsFake, usersFake)
            {
                ControllerContext = FakeControllerContext.GetFakeControllerContextWithFakeIdentity()
            };

            Mapper.CreateMap<Area, AreaShortViewModel>();
            Mapper.CreateMap<Area, AreaListViewModel>();
            Mapper.CreateMap<User, UserShortViewModel>();
            Mapper.CreateMap<Record, RecordListViewModel>();

            ViewResult viewResult = controller.Index() as ViewResult;

            controller.WithCallTo(c => c.Manage(1))
                .ShouldRenderView("Manage")
                .WithModel<AreaManageViewModel>(x => Assert.AreEqual(4, x.Records.Count()));
        }

        //[Test]
        //public void CreateRecordShouldReturnPropperResultIfModelStateIsInvalid()
        //{
        //    var areasFake = new Mock<IAreasServices>();
        //    areasFake.Setup(a => a.GetByUserId(It.IsAny<string>())).Returns(areas);

        //    var recordsFake = new Mock<IRecordsServices>();
        //    recordsFake.Setup(x => x.GetByAreaId(It.IsAny<int>())).Returns(this.records);

        //    var documentsFake = new Mock<IDocumentsServices>().Object;
        //    var usersFake = new Mock<IUsersServices>().Object;

        //    var controller = new AreasController(areasFake.Object, recordsFake.Object, documentsFake, usersFake);
        //    controller.ModelState.AddModelError("Invalid model", "Error message");

        //    Mapper.CreateMap<Area, AreaShortViewModel>();
        //    Mapper.CreateMap<Area, AreaListViewModel>();
        //    Mapper.CreateMap<User, UserShortViewModel>();
        //    Mapper.CreateMap<Record, RecordListViewModel>();

        //    ViewResult viewResult = controller.Index() as ViewResult;
        //    var invalidViewModel = new RecordCreateViewModel();

        //    controller.WithCallTo(c => c.CreateRecord(1, invalidViewModel))
        //        .ShouldRenderView(string.Empty)
        //        .WithModel<RecordCreateViewModel>(x => Assert.AreSame(invalidViewModel, x));
        //}

        [Test]
        public void CreateRecordShouldReturnPropperResultIfModelStateIsValid()
        {
            var areasFake = new Mock<IAreasServices>();
            areasFake.Setup(a => a.GetByUserId(It.IsAny<string>())).Returns(areas);

            var recordsFake = new Mock<IRecordsServices>();

            recordsFake.Setup(x => x.Create(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<RecordStatus>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
                .Returns(new Record()
                {
                    RecordFiles = new List<RecordFile>() { new RecordFile { Path = "~/", RecordId = 1 } }
                });

            var documentsFake = new Mock<IDocumentsServices>();
            documentsFake.Setup<Document>(x => x.GetById(It.IsAny<int>())).Returns(new Document { FilePath = "~/" });

            var usersFake = new Mock<IUsersServices>().Object;

            var controller = new FakeAreasController(areasFake.Object, recordsFake.Object, documentsFake.Object, usersFake)
            {
                ControllerContext = FakeControllerContext.GetFakeControllerContextWithFakeIdentity()
            };

            Mapper.CreateMap<Area, AreaShortViewModel>();
            Mapper.CreateMap<Area, AreaListViewModel>();
            Mapper.CreateMap<User, UserShortViewModel>();
            Mapper.CreateMap<Record, RecordListViewModel>();

            ViewResult viewResult = controller.Index() as ViewResult;
            var validViewModel = new RecordCreateViewModel()
            {
                DocumentId = 1
            };

            controller.WithCallTo(c => c.CreateRecord(1, validViewModel))
                .ShouldRenderView(string.Empty)
                .WithModel<RecordCreateViewModel>(x => Assert.AreSame(validViewModel, x));
        }
    }
}

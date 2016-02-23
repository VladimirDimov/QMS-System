namespace Qms.Tests.RoutesTests.Filters
{
    using AutoMapper;
    using Fakes;
    using Moq;
    using NUnit.Framework;
    using QMS.Models;
    using QMS.Services;
    using QMS.Services.Contracts;
    using QMS.Web.Areas.Private.Controllers;
    using QMS.Web.FIlters;
    using QMS.Web.ViewModels.Areas;
    using QMS.Web.ViewModels.Records;
    using QMS.Web.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using TestStack.FluentMVCTesting;

    class AuthorizeAreasFilterTests
    {
        private object contextFake;

        [Test]
        public void AuthorizeAreasFilterContextResultShouldBeNullIfUserIsInRequiredRole()
        {
            var principalFake = new Mock<IPrincipal>();
            principalFake.Setup(x => x.IsInRole("admin")).Returns(true);

            var httpContextFake = new Mock<HttpContextBase>();
            httpContextFake.Setup(x => x.User).Returns(principalFake.Object);

            var contextFake = new Mock<ActionExecutingContext>();
            contextFake.Setup(x => x.HttpContext).Returns(httpContextFake.Object);

            var authorizeAreaFilter = new AuthorizeArea()
            {
                RolesRequired = new string[] { "admin" }
            };


            authorizeAreaFilter.OnActionExecuting(contextFake.Object);

            Assert.AreEqual(null, contextFake.Object.Result);
        }

        [Test]
        public void AuthorizeAreasFilterContextResultShouldBeNullIfUserDontHaveRoleButOwnesTheArea()
        {
            var principalFake = new Mock<IPrincipal>();
            principalFake.Setup(x => x.IsInRole("admin")).Returns(false);
            principalFake.Setup(x => x.Identity).Returns(new IdentityFake());

            var httpContextFake = new Mock<HttpContextBase>();
            httpContextFake.Setup(x => x.User).Returns(principalFake.Object);

            //Setup fake RouteData
            var routeDataFake = new RouteData();
            routeDataFake.Values.Add("id", "1");

            var requestContextFake = new Mock<RequestContext>();
            requestContextFake.Setup(x => x.RouteData).Returns(routeDataFake);

            var filterContextFake = new Mock<ActionExecutingContext>();
            filterContextFake.Setup(x => x.HttpContext).Returns(httpContextFake.Object);

            var filterContext = filterContextFake.Object;
            filterContext.RequestContext = requestContextFake.Object;

            var userWithId = new User
            {
                Id = "1",
                Areas = new List<Area> { new Area { Id = 1 } }
            };

            var usersServicesFake = new Mock<IUsersServices>();
            usersServicesFake.Setup(x => x.GetById(It.IsAny<string>())).Returns(userWithId);
            var userServices = usersServicesFake.Object;

            var roles = new string[] { "admin" };
            var authorizeAreaFilter = new AuthorizeArea();
            authorizeAreaFilter.RolesRequired = roles;
            authorizeAreaFilter.UsersServices = userServices;

            authorizeAreaFilter.OnActionExecuting(filterContext);

            Assert.AreEqual(null, filterContext.Result);
        }

        [Test]
        public void AuthorizeAreasFilterContextResultShouldNotBeNullIfUserDontHaveRoleAndArea()
        {
            var principalFake = new Mock<IPrincipal>();
            principalFake.Setup(x => x.IsInRole("admin")).Returns(false);
            principalFake.Setup(x => x.Identity).Returns(new IdentityFake());

            var httpContextFake = new Mock<HttpContextBase>();
            httpContextFake.Setup(x => x.User).Returns(principalFake.Object);

            //Setup fake RouteData
            var routeDataFake = new RouteData();
            routeDataFake.Values.Add("id", "1");

            var requestContextFake = new Mock<RequestContext>();
            requestContextFake.Setup(x => x.RouteData).Returns(routeDataFake);

            var filterContextFake = new Mock<ActionExecutingContext>();
            filterContextFake.Setup(x => x.HttpContext).Returns(httpContextFake.Object);

            var filterContext = filterContextFake.Object;
            filterContext.RequestContext = requestContextFake.Object;

            var userWithId = new User
            {
                Id = "1",
            };

            var usersServicesFake = new Mock<IUsersServices>();
            usersServicesFake.Setup(x => x.GetById(It.IsAny<string>())).Returns(userWithId);
            var userServices = usersServicesFake.Object;

            var roles = new string[] { "admin" };
            var authorizeAreaFilter = new AuthorizeArea();
            authorizeAreaFilter.RolesRequired = roles;
            authorizeAreaFilter.UsersServices = userServices;

            authorizeAreaFilter.OnActionExecuting(filterContext);

            var result = filterContext.Result as RedirectResult;
            Assert.AreEqual("~/Account/Login", result.Url);
        }
    }
}

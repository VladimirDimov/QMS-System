namespace Qms.Tests.RoutesTests.Fakes
{
    using Moq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;

    public class FakeControllerContext
    {
        public static ControllerContext GetFakeControllerContextWithFakeIdentity()
        {
            var principalFake = new Mock<IPrincipal>();
            principalFake.Setup(x => x.Identity).Returns(new IdentityFake());

            var fakeServer = new Mock<HttpServerUtilityBase>();
            fakeServer.Setup(x => x.MapPath(It.IsAny<string>())).Returns("MappedPath");

            var fakeHttpContext = new Mock<HttpContextBase>();
            fakeHttpContext.Setup(x => x.User).Returns(principalFake.Object);
            fakeHttpContext.Setup(x => x.Server).Returns(fakeServer.Object);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(x => x.HttpContext).Returns(fakeHttpContext.Object);

            return controllerContext.Object;
        }
    }
}

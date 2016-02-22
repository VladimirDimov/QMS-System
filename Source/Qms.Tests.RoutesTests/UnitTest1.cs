namespace Qms.Tests.RoutesTests
{
    using MvcRouteTester;
    using NUnit.Framework;
    using QMS.Web;
    using QMS.Web.Controllers;
    using System.Net.Http;
    using System.Web.Routing;

    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void ShouldMapHomeControllerIndex()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/Home/Index")
                .To<HomeController>(c => c.Index());
        }
    }
}

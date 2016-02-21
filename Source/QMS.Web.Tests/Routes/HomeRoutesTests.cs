namespace QMS.Web.Tests.Routes
{
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.Controllers;
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    class HomeRoutesTests
    {
        [Test]
        public void HomeControllerShouldMap()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap("/home").To<HomeController>(m => m.Index());
        }
    }
}

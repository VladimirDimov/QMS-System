namespace Qms.Tests.RoutesTests
{
    using MvcRouteTester;
    using NUnit.Framework;
    using QMS.Web;
    using QMS.Web.Areas.Admin.Controllers;
    using QMS.Web.Controllers;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    [TestFixture]
    public class PublicRoutesTests
    {
        [Test]
        public void ShouldMapHomeControllerIndex()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/Home/Index")
                .To<QMS.Web.Controllers.HomeController>(c => c.Index());
        }

        [Test]
        public void ShouldMapCompanyAreasControllerIndexWithNullDepartmentId()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/CompanyAreas/Index")
                .To<QMS.Web.Controllers.CompanyAreasController>(c => c.Index(null));
        }

        [Test]
        public void ShouldMapCompanyAreasControllerIndexWithDepartmentId()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/CompanyAreas/Index/1")
                .To<QMS.Web.Controllers.CompanyAreasController>(c => c.Index(null));
        }

        [Test]
        public void ShouldMapDivisionsControllerIndex()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/Divisions/Index")
                .To<QMS.Web.Controllers.DivisionsController>(c => c.Index());
        }

        [Test]
        public void ShouldMapDocumentsControllerControllerIndexWithoutId()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/Documents/Index")
                .To<QMS.Web.Controllers.DocumentsController>(c => c.Index(null));
        }

        [Test]
        public void ShouldMapDocumentsControllerControllerIndexWithtProcedureId()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/Documents/Index?procedureId=5")
                .To<QMS.Web.Controllers.DocumentsController>(c => c.Index(5));
        }

        [Test]
        public void ShouldMapDocumentsControllerGetFile()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/Documents/GetFile?documentId=1")
                .To<QMS.Web.Controllers.DocumentsController>(c => c.GetFile(1));
        }

        [Test]
        public void ShouldMapProceduresControllerIndex()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection
                .ShouldMap(HttpMethod.Get, "~/Procedures/Index")
                .To<QMS.Web.Controllers.ProceduresController>(c => c.Index());
        }
    }
}

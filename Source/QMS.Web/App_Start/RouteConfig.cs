namespace QMS.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "QMS.Web.Controllers" });

            // Add this code to handle non-existing urls
            routes.MapRoute(
                name: "404-PageNotFound",
                // This will handle any non-existing urls
                url: "{*url}",
                // "Shared" is the name of your error controller, and "Error" is the action/page
                // that handles all your custom errors
                defaults: new { controller = "Errors", action = "Index" });
        }
    }
}

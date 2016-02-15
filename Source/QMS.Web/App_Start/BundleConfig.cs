using System;
using System.Web;
using System.Web.Optimization;

namespace QMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleConfig.RegisterSciptBundles(bundles);
            BundleConfig.RegisterStyleBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
          "~/Content/bootstrap.css",
          "~/Content/toastr.css",
          "~/Content/gridmvc.datepicker.css",
          "~/Content/Gridmvc.css",
          "~/Content/site.css",
          "~/Content/timesheet.min.css"));
        }

        private static void RegisterSciptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/jquery-ui-{version}.js",
            "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
          "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                     "~/Scripts/gridmvc.js"));

            bundles.Add(new ScriptBundle("~/bundles/timesheet").Include(
                        "~/Scripts/timesheet.min.js"));

            //SignalR
            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/jquery.signalR-2.2.0.js"));
        }
    }
}

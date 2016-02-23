﻿namespace QMS.Web
{
    using System.Web.Optimization;

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

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/jquery.signalR-2.2.0.js"));
        }
    }
}

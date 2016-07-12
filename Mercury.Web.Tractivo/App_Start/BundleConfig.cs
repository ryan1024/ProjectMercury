using System.Web;
using System.Web.Optimization;

namespace Mercury.Web.Tractivo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-2.2.3.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/jquery.dropotron.min.js",
                      "~/Scripts/jquery.scrollgress.min.js",
                      "~/Scripts/jquery.scrolly.min.js",
                      "~/Scripts/main.js",
                      "~/Scripts/skel.min.js",
                      "~/Scripts/util.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ie8.css",
                      "~/Content/ie9.css",
                      "~/Content/main.css"));

            bundles.Add(new StyleBundle("~/Content/sass").Include(
                      "~/Content/sass/main.scss",
                      "~/Content/sass/ie8.css",
                      "~/Content/sass/ie9.css"));


        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace KW.Presentation.WebEndPoint
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                          "~/Scripts/libs/jquery-{version}.js"));
                        //"~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/libs/jquery.validate*"));
                        //"~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/libs/modernizr-2.8.3.js"));
                        //"~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/libs/bootstrap.min.js",
                      "~/Scripts/libs/respond-1.4.2.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Scripts/css/bootstrap.min.css",
                      "~/Scripts/css/font-awesome.min.css",
                      "~/Scripts/css/animate.min.css",
                      "~/Scripts/css/theme-default.css",
                      "~/Scripts/css/select2-4.0.3.css",
                      "~/Scripts/css/select2-bootstrap.css",
                      "~/Scripts/css/theme.css",
                      "~/Scripts/css/sweetalert.css",
                      "~/Scripts/css/bootstrap-checkbox.css",
                      "~/Scripts/css/bootstrap-datepicker3.min.css",
                      "~/Scripts/css/style.css",
                      "~/Scripts/css/styles.css",
                      "~/Scripts/css/bootstrap-tagsinput.css",
                      "~/Scripts/css/fieldchooser.css",
                      "~/Scripts/css/bootstrap-multiEmail.css",
                      "~/Scripts/css/simplePagination.css"));

            foreach (var bundle in BundleTable.Bundles)
            {
                bundle.Transforms.Clear();
            }
        }
    }
}

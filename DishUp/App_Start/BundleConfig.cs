using System.Web.Optimization;

namespace DishUp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/jquery-{version}.js",
                 "~/Scripts/jquery.min.js",
                 "~/Scripts/jquery.unobtrusive-ajax.min.js",
                 "~/Scripts/bootstrap.bundle.min.js",
                 "~/Scripts/sb-admin-2.js",
                 "~/Scripts/jquery.easing.min.js"
                 ));
            bundles.Add(new ScriptBundle("~/bundles/Datatable").Include(
                            "~/Scripts/jquery.dataTables.min.js"

                 ));


            bundles.Add(new StyleBundle("~/Content/csss").Include(
                      "~/Content/sb-admin-2.min.css",
                      "~/Content/datatables/dataTables.bootstrap4.min.css",
                      "~/Content/fontawesome-free/css/all.min.css"
                      ));


   


        }
    }
}

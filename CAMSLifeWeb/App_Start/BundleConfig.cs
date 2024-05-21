using System.Web;
using System.Web.Optimization;

namespace CaliphWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/global_assets/js/main/jquery.min.js",
                           "~/global_assets/js/helper/ListHelper.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryplugin").Include(
                     "~/global_assets/js/plugins/forms/inputs/inputmask.js",
                     "~/global_assets/js/plugins/notifications/sweet_alert.min.js",
                     "~/global_assets/js/plugins/forms/inputs/formatter.min.js",
                      "~/global_assets/js/plugins/pickers/pickadate/picker.js",
                       "~/global_assets/js/plugins/pickers/pickadate/picker.date.js",
                        "~/global_assets/js/plugins/pickers/pickadate/picker.time.js",
                         "~/global_assets/js/plugins/extensions/jquery_ui/interactions.min.js",
                          "~/global_assets/js/plugins/forms/selects/select2.min.js",
                           "~/global_assets/js/plugins/ui/fullcalendar/main.min.js",
                            "~/global_assets/js/plugins/ui/dragula.min.js",
                             "~/global_assets/js/plugins/forms/inputs/typeahead/typeahead.bundle.min.js",
                              "~/global_assets/js/plugins/forms/inputs/typeahead/handlebars.min.js",
                                "~/global_assets/js/plugins/ui/moment/moment.min.js",
                                 "~/global_assets/js/plugins/pickers/daterangepicker.js",
                                 "~/global_assets/js/plugins/pickers/datepicker.js",
                                 "~/global_assets/js/plugins/forms/inputs/inputmask.js",
                     "~/global_assets/js/plugins/forms/validation/validate.min.js",
                     "~/global_assets/js/plugins/tables/datatables/datatables.min.js",
                     "~/global_assets/js/plugins/editors/summernote/summernote.min.js",
                        "~/global_assets/js/plugins/loaders/blockui.min.js"));



            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/global_assets/js/main/bootstrap.bundle.min.js", 
                      "~/global_assets//assets/js/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/global_assets/assets/css/all.css",
                      "~/global_assets/css/icons/icomoon/styles.min.css" ));
        }
    }
}

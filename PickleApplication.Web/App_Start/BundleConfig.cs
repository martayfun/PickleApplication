using System.Web;
using System.Web.Optimization;

namespace PickleApplication.Web
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


            bundles.Add(new ScriptBundle("~/bundles/adminJs").Include(
                "~/Scripts/shoppers/angular.min.js",
                "~/Scripts/shoppers/app/common.js",
                "~/Areas/Admin/Content/bootstrap/js/bootstrap.min.js",
                "~/Areas/Admin/Content/inputmask/inputmask.js",
                "~/Areas/Admin/Content/inputmask/inputmask.date.extensions.js",
                "~/Areas/Admin/Content/inputmask/inputmask.extensions.js",
                "~/Areas/Admin/Content/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                "~/Areas/Admin/Content/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Areas/Admin/Content/datatables.net/jquery.dataTables.min.js",
                "~/Areas/Admin/Content/datatables.net-bs/js/dataTables.bootstrap.min.js",
                "~/Areas/Admin/Content/fastclick/js/fastclick.js",
                "~/Areas/Admin/Content/select2/js/select2.min.js",
                "~/Areas/Admin/Scripts/adminlte/adminlte.min.js",
                "~/Areas/Admin/Scripts/adminlte/demo.js",
                "~/Areas/Admin/Content/iCheck/icheck.min.js",
                "~/Areas/Admin/Content/froala-wysiwyg-editor/js/froala_editor.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Js").Include(
                "~/Scripts/shoppers/lodash.js",
                "~/Scripts/shoppers/app/Common.js"));

            bundles.Add(new ScriptBundle("~/bundles/sfCore").Include(
                "~/Scripts/Sf.Core/Core.js",
                "~/Scripts/Sf.Core/Base64.js",
                "~/Scripts/Sf.Core/CookieManager.js",
                "~/Scripts/Sf.Core/LocalStorage.js",
                "~/Scripts/Sf.Core/SessionStorage.js",
                "~/Scripts/Sf.Core/Url.js",
                "~/Scripts/Sf.Core/QueryString.js"));

            bundles.Add(new StyleBundle("~/Content/Css").Include("~/Areas/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                "~/Areas/Admin/Content/bootstrap/css/bootstrap.min.css",
                "~/Areas/Admin/Content/select2/css/select2.min.css",
                "~/Areas/Admin/Content/bootstrap-datepicker/css/bootstrap-datepicker.min.css",
                "~/Areas/Admin/Content/font-awesome/css/font-awesome.min.css",
                "~/Areas/Admin/Content/adminlte/AdminLTE.min.css",
                "~/Areas/Admin/Content/adminlte/skins/_all-skins.min.css",
                "~/Areas/Admin/Content/iCheck/skins/all.css",
                "~/Areas/Admin/Content/datatables.net-bs/css/dataTables.bootstrap.min.css",
                "~/Areas/Admin/Content/froala-wysiwyg-editor/css/froala_editor.min.css",
                "~/Areas/Admin/Content/froala-wysiwyg-editor/css/froala_style.min.css",
                "~/Areas/Admin/Content/froala-wysiwyg-editor/css/plugins/colors.css",
                "~/Areas/Admin/Content/Site.css"));
        }
    }
}

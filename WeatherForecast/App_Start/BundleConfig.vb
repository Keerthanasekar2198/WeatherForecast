Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/external/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/external/jquery.validate*"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/external/modernizr-*"))

        bundles.Add(New Bundle("~/bundles/bootstrap").Include(
                  "~/Scripts/external/bootstrap.js",
                  "~/Scripts/views/index.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/external/bootstrap.css",
                  "~/Content/site.css",
                  "~/Content/views/index.css"))
    End Sub
End Module


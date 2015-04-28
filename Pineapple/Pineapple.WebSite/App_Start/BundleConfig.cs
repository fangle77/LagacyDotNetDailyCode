using System.Web.Optimization;

namespace Pineapple.WebSite.App_Start
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            
        	CommonBundle(bundles);
        	DashboardBundle(bundles);
        	WebSiteBundle(bundles);
        }
        
        private static void CommonBundle(BundleCollection bundles)
        {
        	bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                "~/Content/bootstrap-3.2.0/css/bootstrap.css"
                , "~/Content/bootstrap-3.2.0/css/bootstrap-theme.css"));
        	
        	bundles.Add(new ScriptBundle("~/Content/jquery").Include(
                "~/Content/js/jquery-{version}.js")
                .Include("~/Content/js/jquery.lazyload.js")
                .Include("~/Content/js/jquery.magnific-popup.js"));

            bundles.Add(new ScriptBundle("~/Content/bootstrap/js").Include(
                "~/Content/bootstrap-3.2.0/js/bootstrap.js"));
        }
        
        private static void DashboardBundle(BundleCollection bundles)
        {
        	bundles.Add(new StyleBundle("~/Content/dashboard/css")
                        .Include("~/Content/css/jquery-ui.1.11.1/jquery-ui.css")
                        .Include("~/Content/css/magnific-popup.css")
                        .Include("~/Content/css/dashboard.css")
                        );

            bundles.Add(new ScriptBundle("~/Content/jqueryfileupload/js")
                      .Include("~/Content/js/pineaaple.jquery.fileupload.js"));

            bundles.Add(new ScriptBundle("~/Content/dashboard/js")
                .Include("~/Content/js/jquery-ui-{version}.js")
                .Include("~/Content/js/dashboard/*.js"));
        }
        
        private static void WebSiteBundle(BundleCollection bundles)
        {
        	
        }
    }
}
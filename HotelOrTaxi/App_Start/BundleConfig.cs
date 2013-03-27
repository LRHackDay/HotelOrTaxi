using System.Web.Optimization;

namespace HotelOrTaxi.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/assets/js/bootstrap.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/assets/css/bootstrap-responsive.css",
                "~/assets/css/style.css"));
        }
    }
}
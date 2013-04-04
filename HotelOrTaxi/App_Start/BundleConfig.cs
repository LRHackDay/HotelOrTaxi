using System.Web.Optimization;

namespace HotelOrTaxi.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/assets/js/bootstrap.js",
                "~/assets/js/jquery-1.9.1.js",
                "~/assets/js/autocomplete.js",
                "~/assets/js/location.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/assets/css/bootstrap-responsive.css",
                "~/assets/css/style.css",
                "~/assets/css/style-2.css",
                "~/assets/css/style-3.css",
                "~/assets/css/add2home.css"));
        }
    }
}
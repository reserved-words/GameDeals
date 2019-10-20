using System.Web;
using System.Web.Optimization;

namespace GameDeals
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/GameDeals").Include(
                "~/Scripts/JQuery/jquery-{version}.js",
                "~/Scripts/KnockoutJS/knockout-{version}.js",
                "~/Scripts/KnockoutJS/ko-scrollHandler.js",
                "~/Scripts/MomentJS/moment.min.js",
                "~/Scripts/Bootstrap/bootstrap.js",
                "~/Scripts/oidc-client-js/oidc-client.js",
                "~/Scripts/common.js",
                "~/Scripts/deals.js",
                "~/Scripts/admin.js",
                "~/Scripts/main.js",
                "~/Scripts/auth.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));
        }
    }
}

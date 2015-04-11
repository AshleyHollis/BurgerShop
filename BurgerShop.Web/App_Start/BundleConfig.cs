// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   Copyright © 2015 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Optimization;

namespace BurgerShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css/app").Include("~/content/app.css",
                "~/content/main.css"));

            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/scripts/vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                "~/scripts/vendor/angular-ui-router.js",
                "~/scripts/vendor/toastr.min.js",
                "~/scripts/common/common.js",
                "~/scripts/common/logger.js",
                "~/scripts/common/spinner.js",
                "~/scripts/filters.js",
                "~/scripts/services.js",
                "~/scripts/directives.js",
                "~/scripts/controllers.js",
                "~/scripts/app.js",
                "~/scripts/common/cart.js"));
        }
    }
}

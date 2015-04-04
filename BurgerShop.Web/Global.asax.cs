// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   Copyright © 2015 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using System.Web.Http;
using BurgerShop.Data;

namespace App.BurgerShop.Web
{
    using System.Web;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Set up the Database
            BurgerShopContext burgerShopContext = new BurgerShopContext();
            Database.SetInitializer(new BurgerShopInitializer());
            burgerShopContext.Database.Initialize(true);

        }
    }
}

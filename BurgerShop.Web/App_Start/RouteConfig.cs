// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="">
//   Copyright © 2015 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.BurgerShop.Web
{
    using System.Web.Routing;

    using App.BurgerShop.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add("Default", new DefaultRoute());
        }
    }
}

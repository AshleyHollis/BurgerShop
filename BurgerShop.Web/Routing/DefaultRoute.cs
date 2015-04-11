// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRoute.cs" company="">
//   Copyright © 2015 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Routing;

namespace BurgerShop.Web.Routing
{
    public class DefaultRoute : Route
    {
        public DefaultRoute()
            : base("{*path}", new DefaultRouteHandler())
        {
            this.RouteExistingFiles = false;
        }
    }
}

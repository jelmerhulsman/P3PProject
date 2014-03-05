using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Jan_die_alles_kan.Constraints
{
    public class LocalhostConstraint : IRouteConstraint
    {
        public bool Match
        (
            HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection
        )
    {
        return httpContext.Request.IsLocal;
    }
    }
}
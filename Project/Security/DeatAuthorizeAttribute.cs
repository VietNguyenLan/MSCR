using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project.Security
{
    public class DeatAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            
            var session = (user)HttpContext.Current.Session["user"];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "Admin" }));
            }
            else
            {
                var hasRole = session.role;
                if (hasRole < Order)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Home" }));
                }
                
               
                
            }
        }
    }
}
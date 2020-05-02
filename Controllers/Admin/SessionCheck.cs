using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BuildWebWithDotNetCore.Controllers.Admin
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && 
                filterContext.HttpContext.Session.GetString("isLogin") == null )
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { action = "Index", controller = "Login" }));
            }
        }
    }
}

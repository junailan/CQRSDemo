using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQRSDemo
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/User/Login");
            }

            if (filterContext.HttpContext.User.Identity.Name != "admin")
            {
                filterContext.Result = new RedirectResult("/Home/NoAuthentication");
            }
        }
    }
}
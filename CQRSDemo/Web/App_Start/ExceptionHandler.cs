using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CQRSDemo
{
    public class ExceptionHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            filterContext.Result = new RedirectResult("/Home/Error?message=" + filterContext.Exception.Message);

            filterContext.ExceptionHandled = true;
        }
    }
}
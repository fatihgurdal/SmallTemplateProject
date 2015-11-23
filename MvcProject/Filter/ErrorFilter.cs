using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Filter
{
    public class ErrorFilter: ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Controller.TempData["ex"] = filterContext.Exception;
            filterContext.Result = new RedirectResult("/Error/Detial");
        }
    }
}
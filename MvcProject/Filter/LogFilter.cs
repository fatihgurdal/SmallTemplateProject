using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SmallProject.Business;
using SmallProject.Entity;

namespace MvcProject.Filter
{
    public class LogFilter: ActionFilterAttribute
    {
        //Log iş katmanını oluşturuyoruz.
        private readonly BusinessLog BL;

        public LogFilter()
        {
            BL = new BusinessLog();
        }
        /// <summary>
        /// Eğer bir sayfa Executed olmuşsa o sayfa render edilmiştir ve o sayfayı açabilecek yetkisi vardır.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            BL.AddLog(new Log
            {
                ActionName = filterContext.ActionDescriptor.ActionName,
                ContollerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                UserId =
                    HttpContext.Current.Session["LoginObject"] == null
                        ? 2
                        : ((User) HttpContext.Current.Session["LoginObject"]).Id
            });
        
        }
      
    }
}
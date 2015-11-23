using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Filter;
using SmallProject.Business;

namespace MvcProject.Controllers
{
    public class HomeController : Controller
    {
    
        [ErrorFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}
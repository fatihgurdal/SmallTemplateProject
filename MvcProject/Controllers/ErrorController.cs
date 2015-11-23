using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Detial()
        {
            return View();
        }
        public ActionResult Page404()
        {
            return View();
        }
        public ActionResult Page403()
        {
            return View();
        }
        public ActionResult Page401()
        {
            return View();
        }
    }
}
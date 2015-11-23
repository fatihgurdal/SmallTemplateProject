using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Filter;
using SmallProject.Business;
using SmallProject.Entity;

namespace MvcProject.Controllers
{
    //[LogFilter]
    public class ManagementController : Controller
    {
        private BusinessUser _bUser=new BusinessUser();
        private BusinessUserAuthorities _buAuthorities= new BusinessUserAuthorities();
        private BusinessAuthority _bAuthorities = new BusinessAuthority();
        // GET: Management
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (_bUser.ExistUser(user.UserName, user.Password))
            {
                Session["LoginObject"] = _bUser.GetUser(user.UserName, user.Password);
                return RedirectToAction("Index", "Management");

            }
            ViewBag.ErrorModel = "";
            return View();
        }

        public JsonResult LoginControl()
        {
            return Json((((User) Session["LoginObject"]) != null), JsonRequestBehavior.AllowGet);
        }
        [LogFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            
            ViewBag.user = _bUser.GetUser(1);
            return View();
        }

        public ActionResult GetSideBar()
        {
            IEnumerable<UserAuthorities> Yetkilistesi = _buAuthorities.UserAuthoritieses(1);
            IEnumerable<Authority> Yetkileri = _bAuthorities.GetAuthority(Yetkilistesi);
            return PartialView("_sidebar", Yetkileri);
        }

        public ActionResult SignOut()
        {
            Session["LoginObject"] = null;
            return RedirectToAction("Login","Management");
        }
        [LogFilter]
        [AuthorizationFilter]
        public ActionResult Profile()
        {
            ViewBag.Title = "Profile";
            var user = (User)Session["LoginObject"];
            return View(user);
        }
        [AuthorizationFilter]
        public PartialViewResult GetPageInfo(string ActionName,string ControllerName)
        {
            var authorities = _bAuthorities.GetAuthority(ActionName, ControllerName);
            return PartialView("_PageMap", authorities);
        }
        [AuthorizationFilter]
        public ActionResult UserList()
        {
            return View(_bUser.ListUsers());
        }
        [AuthorizationFilter]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [AuthorizationFilter]
        public ActionResult CreateUser(User user)
        {
           
            if (_bUser.AddUser(user))
            {
                return RedirectToAction("UserList", "Management");
            }
            else
            {
                ViewBag.Error = "Kullanıcı Eklenemedi Lütfen Tekar Deneyiniz";
                return View();
            }
        }
    }
}
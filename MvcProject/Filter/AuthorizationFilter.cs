using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmallProject.Business;
using SmallProject.Entity;

namespace MvcProject.Filter
{
    public class AuthorizationFilter: ActionFilterAttribute, IAuthorizationFilter
    {
        private BusinessUserAuthorities BUAuthorities  { get; }

        public AuthorizationFilter()
        {
            BUAuthorities=new BusinessUserAuthorities();
        }
       
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["LoginObject"] != null)
            {
                // Kullanıcı giriş yapmış demektir. Bilgileri DB'den yinede kontrol etmek isterse keyfin bilir DB ye sorgu atmak çokta anlamlı değil :D :D Performasn  açısından
                var user = (User)HttpContext.Current.Session["LoginObject"];
                if (!BUAuthorities.AuthorityControl(user.Id, filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
            {
                // Kullanıcı giriş yapmamış demektir.
                filterContext.Result = new RedirectResult("/Management/Login");
            }
        }
    }
}
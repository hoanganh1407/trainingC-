using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrainingMVCManagerUser.Controllers
{
    /// <summary>
    /// class filter chặn vào các url khi chưa đăng nhập
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class FilterController : Controller
    {
        /// <summary>
        /// menthod get
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Called after the action method is invoked.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = filterContext.HttpContext.Session["userName"];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                       {
                           { "action", "Index" },
                           { "controller", "Login" },
                           { "area", "Admin" }
                       });
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
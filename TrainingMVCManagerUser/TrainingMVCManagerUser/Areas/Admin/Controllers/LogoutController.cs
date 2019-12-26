using System.Web.Mvc;

namespace TrainingMVCManagerUser.Areas.Admin.Controllers
{
    /// <summary>
    /// class điều khiển việc logout
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class LogoutController : Controller
    {
        /// <summary>
        /// menthod get
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Session.Remove("userName");
            return RedirectToAction("Index","Login", new { area = "Admin" });
        }
    }
}
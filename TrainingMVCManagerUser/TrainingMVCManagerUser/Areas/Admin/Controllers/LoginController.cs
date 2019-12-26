using Model.dao;
using System.Web.Mvc;
using TrainingMVCManagerUser.Areas.Admin.Models;

namespace TrainingMVCManagerUser.Areas.Admin.Controllers
{
    /// <summary>
    /// class điều khiển việc login
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class LoginController : Controller
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
        /// menthod post
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="loginModel"> tên đăng nhập và password</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                UserDao dao = new UserDao();
                bool result = dao.Login(loginModel.UserName, loginModel.Password);
                if (result)
                {
                    Session.Add("userName", loginModel.UserName);
                    return RedirectToAction("ADM002", "ListUser", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError("Login_Fail", Constant.ConstansError.ER016);
                }
            }
            return View("Index");
        }

    }
}
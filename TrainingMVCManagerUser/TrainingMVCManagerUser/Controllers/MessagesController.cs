using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingMVCManagerUser.Controllers
{
    /// <summary>
    /// class điều khiển việc hiển thị message
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class MessagesController : FilterController
    {
        /// <summary>
        /// phương thức get
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public ActionResult Message()
        {
            string message = (string)Session["message"];
            Session.Remove("message");
            ViewBag.message = message;
            return View();
        }
    }
}
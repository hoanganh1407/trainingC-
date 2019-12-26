using Model.dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingMVCManagerUser.Constant;
using TrainingMVCManagerUser.Models;

namespace TrainingMVCManagerUser.Controllers
{
    /// <summary>
    /// class điều khiển ADM005
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class DeleteController : FilterController
    {
        /// <summary>
        /// menthod get
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string iD = Request["ID"];
            if (!String.IsNullOrEmpty(iD))
            {
                UserDao dao = new UserDao();
                tbl_user user = dao.GetUser(iD);
                if(user == null)
                {
                    Session.Add("message", ConstansError.MSG005);
                    return RedirectToAction("Message", "Messages");
                }
                UserModel userModel = Common.Common.Convert(user);
                return View(userModel);
            }
            else
            {
                return RedirectToAction("ADM002", "ListUser");
            }
        }
        /// <summary>
        /// menthod post
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user">user trả về bên view</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(UserModel user)
        {
            UserDao dao = new UserDao();
            bool check = dao.DeleteUser(user.ID);
            if (check)
            {
                Session["message"] = ConstansError.MSG003;
            }
            else
            {
                Session["message"] = ConstansError.MSG005;
            }
            return RedirectToAction("Message", "Messages");
        }
    }
}
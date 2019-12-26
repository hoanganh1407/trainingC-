using Model.dao;
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
    /// điều khiển ADM004
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class ConfirmController : FilterController
    {
        /// <summary>
        /// menthod get
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public ActionResult Confirm()
        {
            UserModel user = (UserModel)Session["user"];
            if (user != null)
            {
                Session.Remove("user");
                return View(user);

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
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Confirm(UserModel user)
        {
            UserDao userDao = new UserDao();
            var test = user;
            string message = String.Empty;
            bool check;
            if (user.ID == 0)
            {
                check = userDao.InsertUser(user);
                if (check)
                {
                    message = ConstansError.MSG001;
                }
            }
            else
            {
                check = userDao.EditUser(user);
                if (check)
                {
                    message = ConstansError.MSG002;
                }
            }
            if (!check)
            {
                message = ConstansError.MSG005;
            }
            Session.Add("message", message);
            return RedirectToAction("Message", "Messages");
        }
    }
}
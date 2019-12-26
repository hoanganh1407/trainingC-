using Model.dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingMVCManagerUser.Models;
using TrainingMVCManagerUser.Common;

namespace TrainingMVCManagerUser.Controllers
{
    /// <summary>
    /// class điều khiển ADM003
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class InsertController : FilterController
    {
        /// <summary>
        /// phương thức get
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InsertUser()
        {
            MstGroupDao groupDao = new MstGroupDao();
            List<mst_group> listGroupDao = groupDao.getAllGroup();
            List<mst_japan> listLevelDao = (new MstJapanDao()).getAllLevel();
            SelectList listYear = Common.Common.CreatYear();
            SelectList listMonth = Common.Common.CreatMonth();
            SelectList listDay = Common.Common.CreatDay();
            List<SelectListItem> listGroupModel = new List<SelectListItem>();
            List<SelectListItem> listLevelModel = new List<SelectListItem>();
            listGroupModel.Add(new SelectListItem
            {
                Value = "0",
                Text = "選択してください"
            });
            foreach (mst_group group in listGroupDao)
            {
                listGroupModel.Add(new SelectListItem
                {
                    Value = group.group_id.ToString(),
                    Text = group.group_name
                });
            }
            listLevelModel.Add(new SelectListItem
            {
                Value = "0",
                Text = "選択してください"
            });
            foreach (mst_japan level in listLevelDao)
            {
                listLevelModel.Add(new SelectListItem
                {
                    Value = level.name_level,
                    Text = level.name_level
                });
            }
            SelectList listGroup = new SelectList(listGroupModel, "Value", "Text");
            SelectList listLevel = new SelectList(listLevelModel, "Value", "Text");
            ViewBag.listGroup = listGroup;
            ViewBag.listYear = listYear;
            ViewBag.listMonth = listMonth;
            ViewBag.listDay = listDay;
            ViewBag.listLevel = listLevel;
            UserModel user = new UserModel
            {
                BirtDayDay = DateTime.Now.Day.ToString(),
                BirtDayMonth = DateTime.Now.Month.ToString(),
                BirtDayYear = DateTime.Now.Year.ToString(),
                StartDateDay = DateTime.Now.Day.ToString(),
                StartDateMonth = DateTime.Now.Month.ToString(),
                StartDateYear = DateTime.Now.Year.ToString(),
                EndDateDay = DateTime.Now.Day.ToString(),
                EndDateMonth = DateTime.Now.Month.ToString(),
                EndDateYear = DateTime.Now.Year.ToString(),
            };
            return View(user);
        }
        /// <summary>
        /// phương thức post
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertUser(UserModel user)
        {
            MstGroupDao groupDao = new MstGroupDao();
            List<mst_group> listGroupDao = groupDao.getAllGroup();
            List<mst_japan> listLevelDao = (new MstJapanDao()).getAllLevel();
            List<SelectListItem> listGroupModel = new List<SelectListItem>();
            List<string> messages = new List<string>();
            if (!user.IsEdit)
            {
                messages = (new Validate()).ValidateUserInfo(user);
            }
            else
            {
                user.BirtDayDay = user.BirtDay.Day.ToString();
                user.BirtDayMonth = user.BirtDay.Month.ToString();
                user.BirtDayYear = user.BirtDay.Year.ToString();

                user.StartDateDay = user.StartDate.Day.ToString();
                user.StartDateMonth = user.StartDate.Month.ToString();
                user.StartDateYear = user.StartDate.Year.ToString();

                user.EndDateDay = user.EndDate.Day.ToString();
                user.EndDateMonth = user.EndDate.Month.ToString();
                user.EndDateYear = user.EndDate.Year.ToString();
            }
            if (messages.Count() != 0 || user.IsEdit)
            {
                user.IsEdit = false;
                List<SelectListItem> listLevelModel = new List<SelectListItem>();
                listGroupModel.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "選択してください"
                });
                foreach (mst_group group in listGroupDao)
                {
                    listGroupModel.Add(new SelectListItem
                    {
                        Value = group.group_id + "",
                        Text = group.group_name
                    });
                }
                listLevelModel.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "選択してください"
                });
                foreach (mst_japan level in listLevelDao)
                {
                    listLevelModel.Add(new SelectListItem
                    {
                        Value = level.name_level,
                        Text = level.name_level
                    });
                }
                SelectList listGroup = new SelectList(listGroupModel, "Value", "Text");
                SelectList listLevel = new SelectList(listLevelModel, "Value", "Text");
                SelectList listYear = Common.Common.CreatYear();
                SelectList listMonth = Common.Common.CreatMonth();
                SelectList listDay = Common.Common.CreatDay();
                ViewBag.listGroup = listGroup;
                ViewBag.listYear = listYear;
                ViewBag.listMonth = listMonth;
                ViewBag.listDay = listDay;
                ViewBag.listLevel = listLevel;
                ViewBag.messages = messages;
                return View("InsertUser", user);
            }
            else
            {
                user.GroupName = groupDao.GetGroupName(user.GroupID);
                Session["user"] = user;
                return RedirectToAction("Confirm", "Confirm");
            }
        }
    }
}
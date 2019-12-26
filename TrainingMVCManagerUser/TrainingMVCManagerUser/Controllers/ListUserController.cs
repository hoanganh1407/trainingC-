using Model.dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingMVCManagerUser.Models;

namespace TrainingMVCManagerUser.Controllers
{
    /// <summary>
    /// class điều khiển ADM002
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class ListUserController : FilterController
    {
        /// <summary>
        /// phương thức get
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)] 
        public ActionResult ADM002()
        {
            string type = Request["type"];
            string sortFullName = "ASC";
            string sortNameLevel = "ASC";
            string sortStartDate = "ASC";
            string sortKey = (string)Session["sortKey"];
            string sortField = (string)Session["sortField"];
            int currentPage = 1;

            List<UserModel> listUser = new List<UserModel>();
            UserDao dao = new UserDao();
            MstGroupDao groupDao = new MstGroupDao();
            if (type != null && type.Equals("sort"))
            {
                sortField = Request["sortField"];
                sortKey = Common.Common.ValidateSort(Request["sortKey"]);
                Session["sortField"] = sortField;
                Session["sortKey"] = sortKey;
            }
            else if (type != null && type.Equals("paging"))
            {
                currentPage = Convert.ToInt32(Request["currentPage"]);
            }
            switch (sortField)
            {
                case "sortFullName":
                    sortFullName = (string)Session["sortKey"];
                    break;
                case "sortNameLevel":
                    sortNameLevel = (string)Session["sortKey"];
                    break;
                case "sortStartDate":
                    sortStartDate = (string)Session["sortKey"];
                    break;
            }
            string fullName = Request["fullName"];
            int groupId = Convert.ToInt32(Request["groupId"]);
            int pageNumber = Convert.ToInt32(Request["pageNumber"]);
            // lay tong so recode
            int totalRecord = dao.GetNumberRecod(fullName, groupId);
            int numberPage = (totalRecord - 1) / 5 + 1;
            // start loop
            int startPage = ((currentPage - 1) / 3) * 3 + 1;
            string typeNextOrBack = Request["nextOrBack"];
            if ("next".Equals(typeNextOrBack))
            {
                startPage += 3;
                currentPage = startPage;
            }
            else if ("back".Equals(typeNextOrBack))
            {
                startPage -= 3;
                currentPage = startPage;
            }
            int endPage = startPage + 3 <= numberPage ? startPage + 2 : numberPage;
            bool isStart = startPage == 1;
            bool isEnd = endPage == numberPage;
            
            //=========================================
            // vừa vào màn hình or search name, group
            List<tbl_user> result = dao.GetAllUser(fullName, groupId, sortField, sortKey, currentPage);
            foreach (tbl_user user in result)
            {
                UserModel userModel = new UserModel()
                {
                    ID = user.user_id,
                    FullName = user.full_name,
                    BirtDay = user.birthday,
                    GroupName = user.mst_group.group_name,
                    Email = user.email,
                    Tel = user.tel,
                    NameLevel = user.tbl_detail_user_japan.Count() == 0? "": user.tbl_detail_user_japan.FirstOrDefault().mst_japan.name_level,
                    StartDate = user.tbl_detail_user_japan.Count() == 0 ? DateTime.Today : user.tbl_detail_user_japan.FirstOrDefault().start_date,
                    Total = user.tbl_detail_user_japan.Count() == 0 ? "" : user.tbl_detail_user_japan.FirstOrDefault().total.ToString(),
                };
                // add 
                listUser.Add(userModel);
            }

            // lay pulldown group
            List<mst_group> listGroupDao = groupDao.getAllGroup();
            List<GroupModel> listGroup = new List<GroupModel>();
            foreach (mst_group group in listGroupDao)
            {
                GroupModel groupModel = new GroupModel()
                {
                    GroupId = group.group_id,
                    groupName = group.group_name
                };
                listGroup.Add(groupModel);
            }
            
            ViewBag.listGroup = listGroup;
            ViewBag.fullName = fullName;
            ViewBag.groupId = groupId;
            ViewBag.sortFullName = sortFullName;
            ViewBag.sortNameLevel = sortNameLevel;
            ViewBag.sortStartDate = sortStartDate;
            ViewBag.sortKey = sortKey;
            ViewBag.pageNumber = pageNumber;
            ViewBag.currentPage = currentPage;
            ViewBag.startPage = startPage;
            ViewBag.endPage = endPage;
            ViewBag.isStart = isStart;
            ViewBag.isEnd = isEnd;
            ViewBag.totalRecord = totalRecord;
            return View(listUser);
        }

    }
}
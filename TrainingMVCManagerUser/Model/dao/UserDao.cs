using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TrainingMVCManagerUser.Models;
using TrainingMVCManagerUser.Common;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Model.dao
{
    /// <summary>
    /// thao tác với bảng tbl_user trong DB
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class UserDao
    {
        public const int RECOD_ON_PAGE = 5;
        TrainingDbContext _db = null;
        /// <summary>
        /// contructor
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        public UserDao()
        {
            _db = new TrainingDbContext();
        }
        /// <summary>
        /// kiểm tra đăng nhập
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="userName">tên đăng nhập</param>
        /// <param name="passWord">password</param>
        /// <returns>true or false</returns>
        public bool Login(string userName, string passWord)
        {
            int result = _db.tbl_user.Count(x => x.login_name == userName && x.password == passWord);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// lấy tất cả user trong DB theo điều kiện
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="fullName">tên truyền vào để search</param>
        /// <param name="groupId">groupId truyền vào để search</param>
        /// <param name="sortField">sortField truyền vào để search</param>
        /// <param name="key_sort">key_sort truyền vào để search</param>
        /// <param name="pageNumber">pageNumber truyền vào để search</param>
        /// <returns>ds user</returns>
        public List<tbl_user> GetAllUser(string fullName, int groupId, string sortField, string key_sort, int pageNumber)
        {
            List<tbl_user> result = new List<tbl_user>();
            bool checkFullName = CommonModel.CheckEmpty(fullName);
            bool checkGroupName = groupId == 0;
            var query = from a in _db.tbl_user
                        select a;

            if (checkGroupName && !checkFullName)
            {
                query = query.Where(x => x.full_name.Contains(fullName)).Select(x => x);
            }
            else if (!checkGroupName && checkFullName)
            {
                query = query.Where(x => groupId == x.mst_group.group_id);
            }
            else if (!checkGroupName && !checkFullName)
            {
                query = query.Where(x => groupId == x.mst_group.group_id && x.full_name.Contains(fullName));
            }
            switch (sortField)
            {
                case "sortFullName":
                    if (key_sort.Equals("ASC"))
                    {
                        query = query.OrderBy(x => x.full_name).Select(x => x);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.full_name).Select(x => x);
                    }
                    break;
                case "sortNameLevel":
                    if (key_sort.Equals("ASC"))
                    {
                        query = query.OrderBy(x => x.tbl_detail_user_japan.FirstOrDefault().mst_japan.name_level);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.tbl_detail_user_japan.FirstOrDefault().mst_japan.name_level);
                    }
                    break;
                case "sortStartDate":
                    if (key_sort.Equals("ASC"))
                    {
                        query = query.OrderBy(x => x.tbl_detail_user_japan.FirstOrDefault().start_date);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.tbl_detail_user_japan.FirstOrDefault().start_date);
                    }
                    break;
                default:
                    query = query.OrderBy(x => x.user_id).Select(x => x);
                    break;
            }
            result = query.ToList();
            return result.Skip((pageNumber - 1) * RECOD_ON_PAGE).Take(RECOD_ON_PAGE).ToList();
        }
        /// <summary>
        /// lấy số lượng record
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="fullName">tên truyền vào để search</param>
        /// <param name="groupId">groupId truyền vào để search</param>
        /// <returns></returns>
        public int GetNumberRecod(string fullName, int groupId)
        {
            bool checkFullName = CommonModel.CheckEmpty(fullName);
            bool checkGroupName = groupId == 0;
            var query = from a in _db.tbl_user
                        select a;

            if (checkGroupName && !checkFullName)
            {
                query = query.Where(x => x.full_name.Contains(fullName)).Select(x => x);
            }
            else if (!checkGroupName && checkFullName)
            {
                query = query.Where(x => groupId == x.mst_group.group_id);
            }
            else if (!checkGroupName && !checkFullName)
            {
                query = query.Where(x => groupId == x.mst_group.group_id && x.full_name.Contains(fullName));
            }
            return query.Count();
        }
        /// <summary>
        /// lấy user theo id
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="id">id truyền vào để search</param>
        /// <returns></returns>
        public tbl_user GetUser(string id)
        {
            var query = from a in _db.tbl_user
                        select a;
            tbl_user user = query.Where(x => x.user_id.ToString().Equals(id)).Select(x => x).FirstOrDefault();
            return user;
        }
        /// <summary>
        /// add thêm 1 user
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user">user truyền vào để add</param>
        /// <returns>true or false</returns>
        public bool InsertUser(UserModel user)
        {
            DbContextTransaction transaction = _db.Database.BeginTransaction();
            try
            {
                MstJapanDao jpDao = new MstJapanDao();
                tbl_user tblUserInsert = this.ConvertToUser(user);

                _db.tbl_user.Add(tblUserInsert);
                _db.SaveChanges();
                tbl_detail_user_japan tbl_detail = new tbl_detail_user_japan();
                string codeLevel = jpDao.getCodeLevel(user.NameLevel);
                if (codeLevel != null)
                {
                    tbl_detail = this.ConvertToDetail(user);
                    tbl_detail.code_level = codeLevel;
                    tbl_detail.user_id = tblUserInsert.user_id;
                    _db.tbl_detail_user_japan.Add(tbl_detail);
                    _db.SaveChanges();
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }
        /// <summary>
        /// update user
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user">user truyền vào để update</param>
        /// <returns>true or false</returns>
        public bool EditUser(UserModel user)
        {
            DbContextTransaction transaction = _db.Database.BeginTransaction();
            try
            {
                MstJapanDao jpDao = new MstJapanDao();
                tbl_user tblUserEdit = _db.tbl_user.Where(x => x.user_id == user.ID).Select(x => x).FirstOrDefault();
                tblUserEdit.full_name = "TestFullName";
                tblUserEdit = this.ConvertToUser(user, tblUserEdit);
                //tblUserEdit.user_id = user.ID;
                _db.tbl_user.AddOrUpdate(tblUserEdit);
                _db.SaveChanges();
                tbl_detail_user_japan tbl_detail = tblUserEdit.tbl_detail_user_japan.FirstOrDefault();
                if (tbl_detail != null && !"0".Equals(user.NameLevel))
                {
                    // edit
                    tbl_detail = this.ConvertToDetail(user, tbl_detail);
                    tbl_detail.code_level = jpDao.getCodeLevel(user.NameLevel);
                    _db.SaveChanges();
                }
                else if (tbl_detail != null && "0".Equals(user.NameLevel))
                {
                    // delete
                    _db.tbl_detail_user_japan.Remove(tbl_detail);
                    _db.SaveChanges();
                }
                else if (tbl_detail == null && !"0".Equals(user.NameLevel))
                {
                    // add
                    tbl_detail = new tbl_detail_user_japan();
                    tbl_detail = this.ConvertToDetail(user, tbl_detail);
                    tbl_detail.code_level = jpDao.getCodeLevel(user.NameLevel);
                    tbl_detail.user_id = tblUserEdit.user_id;
                    _db.tbl_detail_user_japan.Add(tbl_detail);
                    _db.SaveChanges();
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }
        /// <summary>
        /// convert UserModel sang tb_user
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user"></param>
        /// <returns>tbl_user</returns>
        public tbl_user ConvertToUser(UserModel user)
        {
            tbl_user tblUser = new tbl_user();
            tblUser.birthday = user.BirtDay;
            tblUser.email = user.Email;
            tblUser.full_name = user.FullName;
            tblUser.full_name_kana = user.NameKana == null ? "NY" : user.NameKana;
            tblUser.group_id = Int32.Parse(user.GroupID);
            tblUser.login_name = user.LoginUser;
            tblUser.password = user.Password;
            tblUser.tel = user.Tel;
            return tblUser;
        }
        /// <summary>
        /// convert UserModel sang tb_user
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tblUser"></param>
        /// <returns>tbl_user</returns>
        public tbl_user ConvertToUser(UserModel user, tbl_user tblUser)
        {
            tblUser.birthday = user.BirtDay;
            tblUser.email = user.Email;
            tblUser.full_name = user.FullName;
            tblUser.full_name_kana = user.NameKana == null ? "NY" : user.NameKana;
            tblUser.group_id = Int32.Parse(user.GroupID);
            tblUser.tel = user.Tel;
            return tblUser;
        }
        /// <summary>
        /// convert UserModel sang tbl_detail_user_japan
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user"></param>
        /// <returns>tbl_detail_user_japan</returns>
        public tbl_detail_user_japan ConvertToDetail(UserModel user)
        {
            tbl_detail_user_japan tbl_detail = new tbl_detail_user_japan();
            tbl_detail.total = Int32.Parse(user.Total);
            tbl_detail.start_date = user.StartDate;
            tbl_detail.end_date = user.EndDate;
            return tbl_detail;
        }
        /// <summary>
        /// convert UserModel sang tbl_detail_user_japan
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tbl_detail"></param>
        /// <returns>tbl_detail_user_japan</returns>
        public tbl_detail_user_japan ConvertToDetail(UserModel user, tbl_detail_user_japan tbl_detail)
        {
            tbl_detail.total = Int32.Parse(user.Total);
            tbl_detail.start_date = user.StartDate;
            tbl_detail.end_date = user.EndDate;
            return tbl_detail;
        }

        /// <summary>
        /// kiểm tra tên đăng nhập
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="loginName">tên đăng nhập kiểm tra</param>
        /// <returns>true or false</returns>
        public bool CheckExistedLoginName(string loginName)
        {
            tbl_user tblUser = new tbl_user();
            int count = _db.tbl_user.Where(m => m.login_name.Equals(loginName)).Select(m => m).Count();
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// kiểm tra tồn tại Email
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="userId">id user</param>
        /// <param name="email">email cần kiểm tra</param>
        /// <returns>true or false</returns>
        public bool CheckExistedEmail(int userId, string email)
        {
            int count = 0;
            // thêm mới
            if (userId == 0)
            {
                count = _db.tbl_user.Where(x => x.email.Equals(email)).Select(x => x).Count();
            }
            // edit
            else
            {
                count = _db.tbl_user.Where(x => x.email.Equals(email) && x.user_id != userId).Select(x => x).Count();
            }
            if (count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// kiểm tra tồn tại của email
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>true or false</returns>
        public bool DeleteUser(int iD)
        {
            DbContextTransaction transaction = _db.Database.BeginTransaction();
            try
            {
                var tblUserDetailJapanDelete = _db.tbl_detail_user_japan.Where(x => x.user_id == iD).Select(x => x).FirstOrDefault();
                if (tblUserDetailJapanDelete != null)
                {
                    _db.tbl_detail_user_japan.Remove(tblUserDetailJapanDelete);
                }

                var tblUserDelete = _db.tbl_user.Where(x => x.user_id == iD).Select(x => x).FirstOrDefault();
                _db.tbl_user.Remove(tblUserDelete);
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}

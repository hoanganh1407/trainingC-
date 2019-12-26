using Model.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingMVCManagerUser.Constant;
using TrainingMVCManagerUser.Models;

namespace TrainingMVCManagerUser.Common
{
    /// <summary>
    /// class để validate entity UserModel
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class Validate
    {
        /// <summary>
        /// menthod validate UserModel
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="userInfo">object cần validate</param>
        /// <returns>danh sách lỗi string</returns>
        public List<string> ValidateUserInfo(UserModel userInfo)
        {
            MstJapanDao japanDao = new MstJapanDao();
            UserDao tblUser = new UserDao();
            MstGroupDao tblMstGroup = new MstGroupDao();
            // Khởi tạo List để lưu mã lỗi nếu có.
            List<string> arrError = new List<string>();
            // Kiểm tra trường nhập LoginName.
            int userID = userInfo.ID;
            // Trường hợp userId>0 tức trường hợp edit thì không kiểm tra loginName.
            if (userID == 0)
            {
                String loginName = userInfo.LoginUser;
                arrError.AddRange(this.validateLoginName(loginName, tblUser));
            }
            // Kiểm tra trường group
            string groupId = userInfo.GroupID;
            arrError.AddRange(this.validateGroupId(groupId, tblMstGroup));

            // Kiểm tra trường fullname
            String fullName = userInfo.FullName;
            arrError.AddRange(this.validateFullName(fullName));
            // Kiểm tra trường FullNameKana
            String fullNameKana = userInfo.NameKana;
            arrError.AddRange(this.validateFullNameKana(fullNameKana));

            
            // Kiểm tra trường birthday
            try
            {
                userInfo.BirtDay = new DateTime(Int32.Parse(userInfo.BirtDayYear), Int32.Parse(userInfo.BirtDayMonth), Int32.Parse(userInfo.BirtDayDay));
            }
            catch (Exception ex)
            {
                arrError.Add(ConstansError.ERR011_BIRTHDAY);
            }
            

            // Kiểm tra trường email
            String email = userInfo.Email;
            arrError.AddRange(this.validateEmail(email, userID));

            // Kiểm tra trường tel
            String tel = userInfo.Tel;
            arrError.AddRange(this.validateTel(tel));

            // Kiểm tra trường passWord và confirmPassWord.
            if (userID == 0)
            {
                String passWord = userInfo.Password;
                String passWordConfirm = userInfo.PasswordConfirm;
                arrError.AddRange(this.validatePassWord(passWord, passWordConfirm));
            }

            // Thực hiện kiểm tra trường Trình độ tiếng Nhật xem có được chọn hay
            // không. Nếu được chọn ta tiến hành velidate các trường StartDay,
            // EndDay và total
            String levelJapan = userInfo.NameLevel;
            if (!"0".Equals(levelJapan) && !String.IsNullOrEmpty(levelJapan))
            {
                // Kiểm tra codeLevel có tồn tại trong DB không.
                if (string.IsNullOrEmpty(japanDao.getCodeLevel(levelJapan)))
                {
                    arrError.Add(ConstansError.ERR004_CODE_LEVEL);
                }
                // Kiểm tra ngày cấp chứng chỉ.
                // Check ngày không hợp lệ
                try
                {
                    userInfo.StartDate = new DateTime(Int32.Parse(userInfo.StartDateYear), Int32.Parse(userInfo.StartDateMonth), Int32.Parse(userInfo.StartDateDay));
                }
                catch (Exception ex)
                {
                    arrError.Add(ConstansError.ERR011_START_DATE);
                }

                // Kiểm tra ngày hết hạn chứng chỉ.
                // check format của Date
                try
                {
                    userInfo.EndDate = new DateTime(Int32.Parse(userInfo.EndDateYear), Int32.Parse(userInfo.EndDateMonth), Int32.Parse(userInfo.EndDateDay));
                    if (Common.checkEndDate(userInfo.StartDate, userInfo.EndDate))
                    {
                        arrError.Add(ConstansError.ERR012_END_DATE);

                    }
                }
                catch (Exception ex)
                {
                    arrError.Add(ConstansError.ERR011_END_DATE);
                }
                
                // Kiểm tra trường Total.
                string total = userInfo.Total;
                if (String.IsNullOrEmpty(total))
                {
                    arrError.Add(ConstansError.ERR001_TOTAL);
                }else if (!Common.isNumber(total))
                {
                    arrError.Add(ConstansError.ERR018_TOTAL);
                }
            }

            // Trả lại mảng lỗi.
            return arrError;

        }

        /// <summary>
        /// Method dùng để validate cho trường nhập dữ liệu PassWord and
        ///ConfirmPassWord
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="passWord">password cần validate</param>
        /// <param name="passWordConfirm">password confirm</param>
        /// <returns>danh sách lỗi string</returns>
        public List<string> validatePassWord(String passWord, String passWordConfirm)
        {
            List<string> arrError = new List<string>();
            if (String.IsNullOrEmpty(passWord))
            {
                arrError.Add(ConstansError.ERR001_PASSWORD);
            }
            else if (!Common.checkMaxMinLength(passWord, ConstansFormat.MIN_CHAR, ConstansFormat.MAX_CHAR))
            {
                arrError.Add(ConstansError.ERR007_PASSWORD);
            }
            else if (!Common.checkByte(passWord))
            {
                arrError.Add(ConstansError.ERR008_PASSWORD);

            }
            else if (!passWord.Equals(passWordConfirm))
            {
                arrError.Add(ConstansError.ERR017_PASSWORDCONFIRM);
            }
            return arrError;
        }

        /// <summary>
        /// Method dùng để kiểm validate dữ liệu nhập vào trương tel.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="tel">số điện thoại cần validate</param>
        /// <returns>danh sách lỗi string</returns>
        private List<string> validateTel(String tel)
        {
            List<string> arrError = new List<string>();
            String formatTel = ConstansFormat.FORMAT_TEL;
            if (String.IsNullOrEmpty(tel))
            {
                arrError.Add(ConstansError.ERR001_TEL);
            }
            else if (!Common.checkMaxLengthTel(tel))
            {// Kiểm tra maxlength
                arrError.Add(ConstansError.ERR006_TEL);
            }
            else if (!Common.checkFormat(tel, formatTel))
            {
                arrError.Add(ConstansError.ERR011_TEL);
            }
            return arrError;
        }

        /// <summary>
        /// Method dùng để kiểm validate dữ liệu nhập vào trương email.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="email">email cần validate</param>
        /// <param name="userID">id cần validate</param>
        /// <returns>danh sách lỗi string</returns>
        private List<string> validateEmail(String email, int userID)
        {
            List<string> arrError = new List<string>();
            String formatEmail = ConstansFormat.FORMAT_EMAIL;
            if (String.IsNullOrEmpty(email))
            {
                arrError.Add(ConstansError.ERR001_EMAIL);
            }
            else if (!Common.checkMaxLength(email))
            {// Kiểm tra maxlength
                arrError.Add(ConstansError.ERR006_EMAIL);
            }
            else if (!Common.checkFormat(email, formatEmail))
            {
                arrError.Add(ConstansError.ERR011_EMAIL);
            }
            else
            {
                UserDao tblUser = new UserDao();
                if (tblUser.CheckExistedEmail(userID, email))
                {
                    arrError.Add(ConstansError.ERR011_EMAIL_EXIST);
                }
            }
            return arrError;
        }

        /// <summary>
        /// Method dùng để kiểm tra dữ liệp nhập trường fullNameKana.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="fullNameKana">tên tiếng nhật cần check</param>
        /// <returns>danh sách lỗi string</returns>
        private List<string> validateFullNameKana(String fullNameKana)
        {
            List<string> arrError = new List<string>();
            if (!String.IsNullOrEmpty(fullNameKana))
            {
                if (!Common.checkMaxLength(fullNameKana))
                {
                    arrError.Add(ConstansError.ERR006_FULL_NAME_KANA);
                    // Kiểm tra chuỗi nhập vào có phải ký tự kana không.
                }
                else if (!Common.checkFormat(fullNameKana, ConstansFormat.FORMAT_KANA))
                {
                    arrError.Add(ConstansError.ERR009_FULL_NAME_KANA);
                }
            }

            return arrError;
        }

        /// <summary>
        /// Method dùng để kiểm tra dữ liệp nhập trường fullName.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="fullName">tên cần validate</param>
        /// <returns>danh sách lỗi string</returns>
        private List<string> validateFullName(String fullName)
        {
            List<string> arrError = new List<string>();
            if (String.IsNullOrEmpty(fullName))
            {// Kiểm tra không nhập
                arrError.Add(ConstansError.ERR001_FULL_NAME);
            }
            else if (!Common.checkMaxLength(fullName))
            {// Kiểm tra maxlength
                arrError.Add(ConstansError.ERR006_FULL_NAME);
            }
            return arrError;
        }


        /// <summary>
        /// Method dùng để kiểm tra trường lựa chọn GroupID
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="groupId">id của nhóm</param>
        /// <param name="TblMstGroupDao">object thao tác với DB</param>
        /// <returns>danh sách lỗi string</returns>
        public List<string> validateGroupId(string groupId, MstGroupDao TblMstGroupDao)
        {
            List<string> arrError = new List<string>();
            if ("0".Equals(groupId))
            {// Kiểm tra xem có chọn hay
             // không
                arrError.Add(ConstansError.ERR002_GROUP);
                // Kiểm tra sự tồn tại của
            }
            else if (!TblMstGroupDao.CheckExistedGroup(groupId))
            {
                arrError.Add(ConstansError.ERR004_GROUP);
            }

            return arrError;
        }


        /// <summary>
        /// Method dùng để kiểm tra trường loginNam trước khi AddUser.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="loginName">tên đăng nhập</param>
        /// <param name="tblUser">đối tượng thao tác với DB</param>
        /// <returns>danh sách lỗi string</returns>
        public List<string> validateLoginName(string loginName, UserDao tblUser)
        {
            List<string> arrError = new List<string>();
            String format = ConstansFormat.FORMAT_LOGIN_NAME;
            if (String.IsNullOrEmpty(loginName))
            {// Check không nhập
                arrError.Add(ConstansError.ERR001_LOGIN_NAME);
                // Check độ dài loginName
            }
            else if (!Common.checkMaxMinLength(loginName, ConstansFormat.MIN_CHAR_LOGIN, ConstansFormat.MAX_CHAR))
            {
                arrError.Add(ConstansError.ERR007_LOGIN_NAME);
                // check định dạng loginName
            }
            else if (!Common.checkFormat(loginName, format))
            {
                arrError.Add(ConstansError.ERR019_LOGIN_NAME);
            }
            else
            {// check đã tồn tại hay chưa.
                Boolean resutl = tblUser.CheckExistedLoginName(loginName);
                if (resutl)
                {
                    arrError.Add(ConstansError.ERR003_LOGIN_NAME);
                }
            }
            return arrError;
        }

    }
}
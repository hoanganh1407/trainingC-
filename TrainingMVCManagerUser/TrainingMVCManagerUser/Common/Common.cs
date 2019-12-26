using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using TrainingMVCManagerUser.Models;

namespace TrainingMVCManagerUser.Common
{
    /// <summary>
    /// class chứa các phương thức common
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// menthod check rỗng or null
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="vali">string cần check</param>
        /// <returns></returns>
        public static bool CheckEmpty(string vali)
        {
            if (vali == null || vali.Equals(""))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// validate truong asc và des
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static string ValidateSort(string sort)
        {
            if (sort != null && (sort.Equals("ASC") || sort.Equals("DES")))
            {
                return sort;
            }
            return "ASC";
        }
        /// <summary>
        /// Tạo ra danh sách năm
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public static SelectList CreatYear()
        {
            List<SelectListItem> listYear = new List<SelectListItem>();
            var date = DateTime.Now;
            for (int i = date.Year; i >= 1900; i--)
            {
                listYear.Add(
                    new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(),
                    });
            }
            SelectList result = new SelectList(listYear, "Value", "Text");
            return result;
        }
        /// <summary>
        /// tạo danh sách tháng
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public static SelectList CreatMonth()
        {
            List<SelectListItem> listMonth = new List<SelectListItem>();
            var date = DateTime.Now;
            for (int i = 1; i <= 12; i++)
            {
                listMonth.Add(
                    new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(),
                    });
            }
            SelectList result = new SelectList(listMonth, "Value", "Text");
            return result;
        }
        /// <summary>
        /// tạo danh sách ngày
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public static SelectList CreatDay()
        {
            List<SelectListItem> listDay = new List<SelectListItem>();
            var date = DateTime.Now;
            for (int i = 1; i <= 31; i++)
            {
                listDay.Add(
                    new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(),
                    });
            }
            SelectList result = new SelectList(listDay, "Value", "Text");
            return result;
        }
        /// <summary>
        /// convert kiểu dữ liệu từ tbl_user => UserModel
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="user"> object cần convert</param>
        /// <returns></returns>
        public static UserModel Convert(tbl_user user)
        {
            if (user.tbl_detail_user_japan.Count() != 0)
            {
                return new UserModel()
                {
                    ID = user.user_id,
                    BirtDay = user.birthday,
                    Email = user.email,
                    StartDate = user.tbl_detail_user_japan.FirstOrDefault().start_date,
                    EndDate = user.tbl_detail_user_japan.FirstOrDefault().end_date,
                    FullName = user.full_name,
                    GroupID = user.group_id.ToString(),
                    NameKana = user.full_name_kana,
                    LoginUser = user.login_name,
                    Tel = user.tel,
                    NameLevel = user.tbl_detail_user_japan.FirstOrDefault().mst_japan.name_level,
                    Total = user.tbl_detail_user_japan.FirstOrDefault().total.ToString(),
                };
            }
            else
            {
                return new UserModel()
                {
                    ID = user.user_id,
                    BirtDay = user.birthday,
                    Email = user.email,
                    FullName = user.full_name,
                    GroupID = user.group_id.ToString(),
                    NameKana = user.full_name_kana,
                    LoginUser = user.login_name,
                    Tel = user.tel,
                };
            }
        }
        /// <summary>
        /// convet tu 3 string => Datetime
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="year">giá trị năm</param>
        /// <param name="month">giá trị tháng</param>
        /// <param name="day">giá trị ngày</param>
        /// <returns></returns>
        public static DateTime Convert(string year, string month, string day)
        {
            return new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));

        }
        /// <summary>
        /// kiểm tra độ dài thỏa mãn của string
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="str">string cần kiểm tra</param>
        /// <param name="minLength">giá trị min của độ dài</param>
        /// <param name="maxlength">giá trị max của độ dài</param>
        /// <returns></returns>
        public static Boolean checkMaxMinLength(String str, int minLength, int maxlength)
        {
            int length = str.Length;
            if (minLength <= length && length <= maxlength)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check format Strings.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="str">string cần kiểm tra</param>
        /// <param name="format">format cần kiểm tra</param>
        /// <returns></returns>
        public static Boolean checkFormat(string str, string format)
        {
            return Regex.IsMatch(str, format);

        }
        /// <summary>
        /// Check format Date of User.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="date">thời gian cần kiểm tra</param>
        /// <returns></returns>
        public static Boolean checkFormatDate(DateTime date)
        {
            Boolean ketQua = true;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    if (day > 30)
                    {
                        ketQua = false;
                    }
                    break;
                case 2:
                    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                    {
                        if (day > 29)
                        {
                            ketQua = false;
                        }
                    }
                    else if (day > 28)
                    {
                        ketQua = false;
                    }
                    break;
            }

            return ketQua;
        }
        /// <summary>
        /// Tiến hành check EndDate và StartDate.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="startDate">ngày bắt đầu</param>
        /// <param name="endDate">ngày kết thúc</param>
        /// <returns></returns>
        public static Boolean checkEndDate(DateTime startDate, DateTime endDate)
        {
            Boolean ketQua = false;
            if (startDate >= endDate)
            {
                ketQua = true;
            }
            return ketQua;
        }
        /// <summary>
        /// Check Number String
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="total">kiểm tra điểm là kiểu số</param>
        /// <returns></returns>
        public static Boolean isNumber(String total)
        {
            String format = "[0-9]+";
            return Regex.IsMatch(total, format);
        }
        /// <summary>
        /// Check số byte của char.
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="password">kiểm tra password</param>
        /// <returns></returns>
        public static Boolean checkByte(String password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                char c = password[i];
                if (c < 0 || c > 255)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Check length of tel
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="str">kiểm tra số điện thoại</param>
        /// <returns></returns>
        public static Boolean checkMaxLengthTel(String str)
        {
            if (str.Length <= 14)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check maxlength of strings
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="str">kiểm tra độ dài max</param>
        /// <returns></returns>
        public static Boolean checkMaxLength(String str)
        {
            if (str.Length <= 255)
            {
                return true;
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingMVCManagerUser.Constant
{
    /// <summary>
    /// chứa các hằng số về regex, đường dẫn, message.
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public static class ConstansFormat
    {
        // Giới hạn bản ghi trên 1 page và limit paging.
        public const int LIMIT_RECORD = 5;
        public const int LIMIT_PAGE = 3;
        // Default sắp xếp
        public const String DEFAULT_SORT_TYPE = "full_name";
        public const String DEFAULT_FULL_NAME_SORT = "ASC";
        public const String DEFAULT_CODE_LEVEL_SORT = "ASC";
        public const String DEFAULT_END_DATE_SORT = "DESC";
        // Trường phân biệt user với admin.
        public const int CATEGORY_ADMIN = 0;
        public const int CATEGORY_USER = 1;
        // Name column sử dụng khi sắp xếp.
        public const String FULL_NAME = "full_name";
        public const String CODE_LEVEL = "code_level";
        public const String END_DATE = "end_date";
        // Message của hệ thống
        public const String MSG001 = "MSG001";
        public const String MSG002 = "MSG002";
        public const String MSG003 = "MSG003";
        public const String MSG004 = "MSG004";
        public const String MSG005 = "MSG005";
        public const String MSG006 = "MSG006";
        // Message Error của hệ thống
        public const String ER015 = "ER015";
        public const String ER013 = "ER013";

        public const int START_YEAR = 1980;
        public const String ERROR = "error";

        // Format check định dạng trường.
        public const String FORMAT_LOGIN_NAME = "^[a-zA-Z_][a-zA-Z0-9_]+";
        public const String FORMAT_EMAIL = "[a-zA-Z][\\w]+@[a-zA-Z]+\\.[a-zA-Z]+";
        public const String FORMAT_TEL = "[0-9]{1,4}-[0-9]{1,4}-[0-9]{1,4}";
        // Giới hạn chuỗi ký tự
        public const int MIN_CHAR_LOGIN = 4;
        public const int MIN_CHAR = 5;
        public const int MAX_CHAR = 15;

        public const String SUCCESS = "/Success.do";
        public const String INSERT_SUCCESS = "success";
        public const String SYSTEM_ERROR = "system_error";
        public const String FORMAT_KANA = "[ァ-ン]+";
        public const String INSERT = "insert";
        public const String UPDATE = "update";
        public const String UPDATE_SUCCESS = "updateSuccess";
        public const String DELETE_SUCCESS = "deleteSuccess";
        public const String CHANE_SUCCESS = "chaneSuccess";

        public const int LENGTH_SALT = 6;

        public const String ERR001_USERNAME = "ERR001_UserName";
        public const String ERR001_PASS = "ERR001_Pass";
        public const String ERR016 = "ERR016";
        public const String NOT_FOUND_USER = "ERR013";
        public const String DELETE_USER = "deleteUser";
        public const String CHANE_PASS = "chanePass";
        public const int RECOD_ON_PAGE = 5;
    }
}
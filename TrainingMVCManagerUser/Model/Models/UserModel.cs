using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingMVCManagerUser.Models
{
    /// <summary>
    /// Entity UserModel
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class UserModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime BirtDay { get; set; }

        public string BirtDayYear { get; set; }
        public string BirtDayMonth { get; set; }
        public string BirtDayDay { get; set; }
        public string GroupName { get; set; }
        public string GroupID { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string NameLevel { get; set; }
        public string CodeLevel { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateYear { get; set; }
        public string StartDateMonth { get; set; }
        public string StartDateDay { get; set; }
        public string Total { get; set; }

        public string LoginUser { get; set; }
        public string NameKana { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDateYear { get; set; }
        public string EndDateMonth { get; set; }
        public string EndDateDay { get; set; }
        public bool IsEdit { get; set; }

    }
}
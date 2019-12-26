using System.ComponentModel.DataAnnotations;

namespace TrainingMVCManagerUser.Areas.Admin.Models
{
    /// <summary>
    /// entity LoginModel
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage = Constant.ConstansError.ER001_USER_NAME)]
        public string UserName { get; set; }
        [Required(ErrorMessage = Constant.ConstansError.ER001_PASSWORD)]
        public string Password { get; set; }
    }
}
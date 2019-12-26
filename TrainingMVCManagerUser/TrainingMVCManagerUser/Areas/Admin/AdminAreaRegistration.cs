using System.Web.Mvc;

namespace TrainingMVCManagerUser.Areas.Admin
{
    /// <summary>
    /// class tạo ra vùng Areal Admin
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class AdminAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// menthod đặt tên cho vung Areal 
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }
        /// <summary>
        /// menthod đăng kí areal 
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
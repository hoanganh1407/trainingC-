using System.Web.Mvc;
using System.Web.Routing;

namespace TrainingMVCManagerUser
{
    /// <summary>
    /// class cấu hình cho việc route các url
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// đăng kí việc route
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

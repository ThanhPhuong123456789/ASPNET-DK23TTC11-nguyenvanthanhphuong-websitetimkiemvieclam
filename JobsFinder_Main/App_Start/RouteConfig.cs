using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JobsFinder_Main
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "profile/ho-so-ca-nhan",
                defaults: new { controller = "Profile", action = "index", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "User",
                url: "tai-khoan/Update",
                defaults: new { controller = "User", action = "Update", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "Job",
                url: "cong-viec",
                defaults: new { controller = "Job", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "Company",
                url: "cong-ty",
                defaults: new { controller = "Company", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "Job Details",
                url: "cong-viec/{Code}-{id}",
                defaults: new { controller = "Job", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "Company Details",
                url: "cong-ty/{MetaTitle}-{id}",
                defaults: new { controller = "Company", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "JobsFinder_Main.Controllers" }
            );
        }
    }
}

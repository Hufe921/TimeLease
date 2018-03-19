using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TimeLease.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var attr = new EnableCorsAttribute("*", "*", "*");
            attr.SupportsCredentials = true;
            // Web API 配置和服务
            config.EnableCors(attr);
            //记录日志
            log4net.Config.XmlConfigurator.Configure();
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

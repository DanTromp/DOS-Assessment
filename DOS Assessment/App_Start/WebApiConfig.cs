using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace DOS_Assessment
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Replacing default exception handler with custom one
            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public class UnhandledExceptionLogger : ExceptionLogger
        {
            public override void Log(ExceptionLoggerContext context)
            {
                var log = context.Exception.ToString();
                Console.WriteLine("Error: " + log);
            }
        }
    }
}

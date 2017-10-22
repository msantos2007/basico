using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Basico.Web.App_Start;
using Basico.Web.Infrastructure.Core;


namespace Basico.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            var config = GlobalConfiguration.Configuration;

            AreaRegistration.RegisterAllAreas();
             

            WebApiConfig.Register(config);
            Bootstrapper.Run();            
            GlobalConfiguration.Configuration.EnsureInitialized();
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            UrlRewrite.RewriteModule.Initialize();
            
            //JobScheduler.Start();
        }

        //Teste pode remover
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    var context = HttpContext.Current;
        //    var response = context.Response;

        //    // enable CORS
        //    response.AddHeader("Access-Control-Allow-Origin", "*");
            
        //}
    }


    public class GlobalDateTime
    {
        //stackoverflow.com/questions/16490463/how-is-local-determined-in-tolocaltime
        //stackoverflow.com/questions/2532729/daylight-saving-time-and-time-zone-best-practices

        public static TimeZoneInfo _tzi = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        public static DateTime LocalNow()  //De UTC para Local Brasilia
        {
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, _tzi);
        }

        public static DateTime NoonLocal()
        {
            DateTime converted = TimeZoneInfo.ConvertTime(DateTime.UtcNow, _tzi).Date;
            return new DateTime(converted.Year, converted.Month, converted.Day, 12, 0, 0, 0);
        }

        public static DateTime NoonUTC()
        {
            DateTime converted = DateTime.UtcNow.Date;
            return new DateTime(converted.Year, converted.Month, converted.Day, 12, 0, 0, 0); ;
        }

        public static DateTime UTC2Local(DateTime utc)
        {
            return TimeZoneInfo.ConvertTime(utc, _tzi);
        }

        public static DateTime Local2UTC(DateTime local)
        {
            return local.ToUniversalTime();
        }

        public static DateTime Any2Noon(DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day, 12, 0, 0, 0, DateTimeKind.Unspecified);
        }
    }
}

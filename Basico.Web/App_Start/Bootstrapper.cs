using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Basico.Web.Mappings;

namespace Basico.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }
    }
}
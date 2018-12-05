using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(USoftEducation.Startup))]

namespace USoftEducation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //auth config, service registration, etc      
            //var config = new HttpConfiguration();
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            ////other config settings, dependency injection/resolver settings, etc
            //app.UseWebApi(config);
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling =
            //    Newtonsoft.Json.PreserveReferencesHandling.All;
            ConfigureAuth(app);
        }
    }
}

using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using MyApp.Data;
using MyApp.WebMS.Factories;
using Serilog;
using Serilog.Sinks.Seq;
using Serilog.Configuration;
using System.Configuration;

namespace MyApp.WebMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var seqServerUrl = ConfigurationManager.AppSettings["SeqServerUrl"];
            var logConfig = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .Enrich.FromLogContext()
           .WriteTo.Seq(seqServerUrl);

            Log.Logger = logConfig.CreateLogger();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterCustomControllerFactory();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

        
        }



        private void RegisterCustomControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
        }


    }
}

namespace Tfs.WebHooks
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using Diagnostics;
    using Handlers;
    using MessageHandlers;
    using Microsoft.AspNet.WebHooks;
    using Microsoft.Owin.BuilderProperties;
    using Owin;
    using Serilog;
    using System;
    using System.Configuration;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using WebHookHandlers;
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureSerilog();
            // Configure Web API for self - host.
            HttpConfiguration config = new HttpConfiguration();
            //  Enable attribute based routing
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
            var container = GetContainer();

            var configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            configuration.MessageHandlers.Add(new SerilogRequestIdHandler());
            configuration.InitializeReceiveVstsWebHooks();

            app.UseWebApi(configuration);

            new AppProperties(app.Properties)
                .OnAppDisposing
                .Register(() =>
                {
                    Log.CloseAndFlush();
                });
            //app.UseNinjectMiddleware(CreateKernel);
            //app.UseNinjectWebApi(config);
        }

        private static void ConfigureSerilog()
        {
            var environment = ConfigurationManager.AppSettings["Environment"];
            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{RequestId}] [{Level:u3}] {Message}{NewLine}{Exception}";
            var filePath = ConfigurationManager.AppSettings["LogFilePath"];

            var flushToDiskIntervalInSeconds = 10;
            if (int.TryParse(ConfigurationManager.AppSettings["LogFileFlushToDiskIntervalInSeconds"], out int configuredFlushToDiskIntervalInSeconds))
            {
                flushToDiskIntervalInSeconds = configuredFlushToDiskIntervalInSeconds;
            }

            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(
                    filePath,
                    outputTemplate: outputTemplate,
                    flushToDiskInterval: TimeSpan.FromSeconds(flushToDiskIntervalInSeconds));

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            // WebHooks handlers
            builder
                .RegisterType<VstsWebHookHandler>()
                .As<IWebHookHandler>();

            // Handlers
            builder
                .Register(x => new UpdatedPullRequestHandler())
                .AsSelf()
                .SingleInstance();
            
            // Loggers
            builder
                .RegisterType<WebHooksSerilogLogger>()
                .As<Microsoft.AspNet.WebHooks.Diagnostics.ILogger>()
                .SingleInstance();

            builder
                .RegisterType<SerilogExceptionLogger>()
                .As<IExceptionLogger>()
                .SingleInstance();

            return builder.Build();
        }

        //public static IKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();

        //    kernel.Bind<IRequestHandler>().To<UpdateService>();
        //    kernel.Bind<IBotService>().To<BotService>();
        //    return kernel;
        //}
    }
}
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Hub.BackgroundJob.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)

            .Enrich.FromLogContext()
            .WriteTo.File(Environment.CurrentDirectory + @"\Logs\" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt",
                fileSizeLimitBytes: 1_000_000,
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1)
                , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            //CreateWebHostBuilder(args).Build().Run();

            var host = Host.CreateDefaultBuilder(args)
               .UseSerilog()
               .UseServiceProviderFactory(new AutofacServiceProviderFactory())
               .ConfigureWebHostDefaults(webHostBuilder => {
                   webHostBuilder
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseIISIntegration()
                 .ConfigureAppConfiguration(
                     configHost =>
                     {
                         configHost.SetBasePath(Directory.GetCurrentDirectory());
                         configHost.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                         configHost.AddJsonFile($"appsettings.{ GetEnvironmentName()}.json", optional: false, reloadOnChange: true);
                         configHost.AddCommandLine(args);

                     }
                     )
             .UseStartup<Startup>();
               })
               .Build();

            host.Run();
        }

        /// <summary>
        /// Gets the name of the environment.
        /// </summary>
        /// <returns></returns>
        public static string GetEnvironmentName()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (Environments.Production == environmentName || string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = "prod";
            }
            return environmentName;
        }
    }
}


//public class Program
//{
//    public static string Namespace = typeof(Startup).Namespace;
//    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
//}


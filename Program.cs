using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace CSharpCoreWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cannot find the appsetttings.json when you build a single file exe
            // https://github.com/dotnet/runtime/issues/36065
            // https://github.com/dotnet/extensions/blob/275e691f7e575f208290d1cbb8cb450f4a3a85d6/src/Hosting/Hosting/src/HostingHostBuilderExtensions.cs#L35-L41
            var errorLogConfiguration = new ConfigurationBuilder()
                //.SetBasePath(Process.GetCurrentProcess().MainModule.FileName)
                // gets > 'C:\WINDOWS\system32\appsettings.json'
                //.SetBasePath(Directory.GetCurrentDirectory())
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(errorLogConfiguration)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseWindowsService()
                .ConfigureAppConfiguration((context, config) =>
                {

                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker1>();
                    services.AddHostedService<Worker2>();
                })
                .UseSerilog();
    }
}

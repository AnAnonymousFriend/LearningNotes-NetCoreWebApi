using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
           //.MinimumLevel.Information()
           //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
           //.Enrich.FromLogContext()
           .ReadFrom.Configuration(new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build())
           .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                       Host.CreateDefaultBuilder(args)
                      .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //<--NOTE THIS  ÒÀÀµ×¢Èë
                      .ConfigureWebHostDefaults(webBuilder =>
                      {
                          webBuilder.UseStartup<Startup>();            
                      });
    }
}

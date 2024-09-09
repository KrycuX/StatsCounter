using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace StatsCounter
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) => { builder.AddUserSecrets(Assembly.GetExecutingAssembly(), true); })
                .UseStartup<Startup>();
    }
}
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StatsCounter.Extensions;
using StatsCounter.Services;
#nullable enable
namespace StatsCounter
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var gitHubKey = _configuration["Github:GithubKey"];
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "StatsCounter API",
                        Version = "v1"
                    });
            });

            services
                .AddGitHubService(new Uri(_configuration["GitHubSettings:BaseApiUrl"]),gitHubKey)
                .AddTransient<IStatsService, StatsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); })
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StatsCounter API v1");
                    c.RoutePrefix = string.Empty;
                });
        }
    }
}
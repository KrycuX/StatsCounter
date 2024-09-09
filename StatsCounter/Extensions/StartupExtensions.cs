using Microsoft.Extensions.DependencyInjection;
using StatsCounter.Services;
using System;
using System.Net.Http;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services,
            Uri baseApiUrl,
            string token)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = baseApiUrl;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            services.AddSingleton<IGitHubService>(sp => new GitHubService(httpClient));
            return services; // TODO: add your code here
        }
    }
}
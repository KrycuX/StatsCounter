using StatsCounter.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StatsCounter.Services
{
    public interface IGitHubService
    {
        Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwner(string owner);
    }

    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwner(string owner)
        {
            try
            {
                var result = await _httpClient.GetAsync($"users/{owner}/repos");
                string JsonString = await result.Content.ReadAsStringAsync();
                List<RepositoryInfo>? repositoryInfos = JsonSerializer.Deserialize<List<RepositoryInfo>>(JsonString);


                if (repositoryInfos == null)
                    return new List<RepositoryInfo>();

                return repositoryInfos;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Sample_Message", ex);
            }
           


        }
    }
}

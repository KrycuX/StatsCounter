using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsCounter.Models;
using static System.Net.Mime.MediaTypeNames;

namespace StatsCounter.Services
{
    public interface IStatsService
    {
        Task<RepositoryStats> GetRepositoryStatsByOwner(string owner);
    }

    public class StatsService : IStatsService
    {
        private readonly IGitHubService _gitHubService;

        public StatsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<RepositoryStats> GetRepositoryStatsByOwner(string owner)
        {
            var reposInfo = await _gitHubService.GetRepositoryInfosByOwner(owner);
         
            Dictionary<char, int> wynik = GetLetters(reposInfo.Select(x=>x.Name));
            var SumStag = reposInfo.Sum(x => x.StargazersCount);
            var avgStag = SumStag / reposInfo.Count();
            var SumWatch = reposInfo.Sum(x => x.WatchersCount);
            var avgWatch = SumWatch / reposInfo.Count();
            var SumForks = reposInfo.Sum(x => x.ForksCount);
            var avgForks = SumForks / reposInfo.Count();
            var SumSize = reposInfo.Sum(x => x.Size);
            var avgSize = SumSize / reposInfo.Count();

            RepositoryStats repositoryStats = new RepositoryStats(owner, wynik, avgStag, avgWatch, avgForks, avgSize);

            return repositoryStats;
        }

        private Dictionary<char, int> GetLetters(IEnumerable<string> enumerable)
        {
            Dictionary<char, int> licznikLiter = new Dictionary<char, int>();


            foreach (string tekst in enumerable)
            {

                foreach (char litera in tekst.ToLower())
                {
                    if (licznikLiter.ContainsKey(litera))
                    {
                        licznikLiter[litera]++;
                    }
                    else
                    {
                        licznikLiter[litera] = 1;
                    }
                }
            }

            return licznikLiter;
        }
    }
}
using System.Collections.Generic;

namespace StatsCounter.Models
{
    public record RepositoryStats(
        string Owner,
        IDictionary<char, int> Letters,
        double AvgStargazers,
        double AvgWatchers,
        double AvgForks,
        double AvgSize);
}
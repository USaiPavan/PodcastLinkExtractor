using System.Threading.Tasks;
using PodcastLinkExtractor.Entities;

namespace PodcastLinkExtractor.Contracts
{
    public interface ILinkExtractor
    {
        int LatestDownloadedEpisode { get; }
        Task<Links> GetLinks(int startEpisode, int? endEpisode = null);
        Task SaveLinks(Links episodeLinks);
    }
}
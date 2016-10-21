using System.Threading.Tasks;
using PodcastLinkExtractor.Entities;

namespace PodcastLinkExtractor.Contracts
{
    public interface IShowLinksContext
    {
        Task AddLinks(Links links);
    }
}
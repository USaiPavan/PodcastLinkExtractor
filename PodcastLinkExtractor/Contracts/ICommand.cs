using System.Threading.Tasks;

namespace PodcastLinkExtractor.Contracts
{
    public interface ICommand
    {   
        Task<bool> Execute();
    }
}
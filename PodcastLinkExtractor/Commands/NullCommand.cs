using System.Threading.Tasks;
using PodcastLinkExtractor.Contracts;

namespace PodcastLinkExtractor.Commands
{
    public class NullCommand : ICommand
    {
        public Task<bool> Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
using Microsoft.Practices.Unity;
using PodcastLinkExtractor.Entities;
using PodcastLinkExtractor.Factories;
using PodcastLinkExtractor.Infrastructure;

namespace PodcastLinkExtractor
{
    internal class Program
    {   
        private static void Main(string[] args)
        {
            Application.RegisterDependencies();
            var command = Parser.Process(args);
            command.Execute();
        }
    }
}

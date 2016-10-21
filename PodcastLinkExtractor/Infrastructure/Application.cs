using Microsoft.Practices.Unity;
using PodcastLinkExtractor.Commands;
using PodcastLinkExtractor.Contracts;
using PodcastLinkExtractor.Data;

namespace PodcastLinkExtractor.Infrastructure
{
    public static class Application
    {
        public static IUnityContainer CurrentContainer { get; private set; }

        public static void RegisterDependencies()
        {
            CurrentContainer = new UnityContainer();
            CurrentContainer.RegisterInstance<IShowLinksContext>(new PodcastShowLinksContext());
            CurrentContainer.RegisterType<ICommand, ProcessShowLinksCommand>(Constants.PROCESS_SHOW_OPTION);
        }

        public static void Reset()
        {
            CurrentContainer = null;
        }
    }
}
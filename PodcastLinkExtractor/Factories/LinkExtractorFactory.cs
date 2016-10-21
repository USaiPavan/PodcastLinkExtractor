using System;
using Microsoft.Practices.Unity;
using PodcastLinkExtractor.Contracts;
using PodcastLinkExtractor.Entities;
using PodcastLinkExtractor.Infrastructure;

namespace PodcastLinkExtractor.Factories
{
    public static class LinkExtractorFactory
    {
        public static ILinkExtractor GetExtractorInstance(Podcast podcastName)
        {
            switch (podcastName)
            {
                case Podcast.DotNetRocks:
                    return new DotNetRocksLinkExtractor(Application.CurrentContainer.Resolve<IShowLinksContext>());
                case Podcast.CodingBlocks:
                    throw new NotImplementedException();
                case Podcast.Hanselminutes:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(podcastName), podcastName, null);
            }
        }
    }
}
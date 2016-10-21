using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PodcastLinkExtractor.Contracts;
using PodcastLinkExtractor.Entities;

namespace PodcastLinkExtractor.Factories
{
    public class DotNetRocksLinkExtractor : LinkExtractor
    {
        public override int LatestDownloadedEpisode { get; }
        private const Podcast CURRENT_PODCAST = Podcast.DotNetRocks;
        private const string BASE_URL = "https://www.dotnetrocks.com/Home/ShowDetailsModal/";

        private readonly List<string> _forbiddenLinks = new List<string>() { "mtcb" };

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetRocksLinkExtractor"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DotNetRocksLinkExtractor(IShowLinksContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <param name="startEpisode">The start episode.</param>
        /// <param name="endEpisode">The end episode.</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Received HTML is not in expected format</exception>
        public override async Task<Links> GetLinks(int startEpisode, int? endEpisode = null)
        {
            var links = new Links();
            endEpisode = endEpisode ?? 1;

            for (var episodeIndex = startEpisode; episodeIndex >= endEpisode; episodeIndex--)
            {
                var episodeUrl = $"{BASE_URL}{episodeIndex}";
                var pageContent = await GetPageContent(episodeUrl);
                var selectedSpanNodes = SelectedSpanNodes(pageContent);

                foreach (var selectedNode in selectedSpanNodes)
                {
                    var showLink = GetFirstAnchorNode(selectedNode);
                    if (IsInvalidShowLink(showLink)) continue;

                    var linkInfo = new LinkInformation()
                    {
                        EpisodeNumber = episodeIndex,
                        EpisodeTitle = pageContent.DocumentNode.SelectSingleNode("/html[1]/head[1]/title[1]").InnerText.Trim(),
                        PodcastName = CURRENT_PODCAST,
                        Title = HtmlEntity.DeEntitize(showLink?.InnerText)?.Trim(),
                        Url = showLink?.Attributes["href"].Value.Trim()
                    };

                    links.Add(linkInfo);
                }
            }

            return links;
        }

        /// <summary>
        /// Gets the first anchor node.
        /// </summary>
        /// <param name="selectedNode">The selected node.</param>
        /// <returns></returns>
        private static HtmlNode GetFirstAnchorNode(HtmlNode selectedNode)
        {
            return selectedNode.Descendants("a").FirstOrDefault();
        }


        /// <summary>
        /// Determines whether [is invalid show link] [the specified show link].
        /// </summary>
        /// <param name="showLink">The show link.</param>
        /// <returns>
        ///   <c>true</c> if [is invalid show link] [the specified show link]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsInvalidShowLink(HtmlNode showLink)
        {
            if (showLink == null)
            {
                return true;
            }

            if (showLink.Attributes["href"].Value.Contains("http") == false ||
                _forbiddenLinks.Any(link => showLink.Attributes["href"].Value.Contains(link)))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Selecteds the span nodes.
        /// </summary>
        /// <param name="pageContent">Content of the page.</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Received HTML is not in expected format</exception>
        private static IEnumerable<HtmlNode> SelectedSpanNodes(HtmlDocument pageContent)
        {
            var selectedSpanNodes =
                pageContent.DocumentNode
                    .Descendants("div")
                    .FirstOrDefault(
                        d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("comments"))
                    ?.Descendants("span");

            if (selectedSpanNodes == null)
            {
                throw new InvalidDataException("Received HTML is not in expected format");
            }
            return selectedSpanNodes;
        }
    }
}
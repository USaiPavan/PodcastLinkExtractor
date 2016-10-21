using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PodcastLinkExtractor.Contracts;
using PodcastLinkExtractor.Entities;

namespace PodcastLinkExtractor.Factories
{
    public abstract class LinkExtractor : ILinkExtractor
    {
        public virtual int LatestDownloadedEpisode { get; }
        protected readonly IShowLinksContext _context;

        protected LinkExtractor(IShowLinksContext context)
        {
            _context = context;
        }

        public abstract Task<Links> GetLinks(int startEpisode, int? endEpisode = null);

        protected async Task<HtmlDocument> GetPageContent(string url)
        {
            try
            {
                var content = string.Empty;
                using (var client = new HttpClient())
                using (var response = await client.GetAsync(url))
                using (var responseContent = response.Content)
                {
                    content = await responseContent.ReadAsStringAsync();
                }
                
                var currentPage = new HtmlDocument();
                currentPage.LoadHtml(content);
                return currentPage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        public virtual async Task SaveLinks(Links episodeLinks)
        {
            await _context.AddLinks(episodeLinks);
        }
    }
}
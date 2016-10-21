using System.Threading.Tasks;
using MongoDB.Driver;
using PodcastLinkExtractor.Contracts;
using PodcastLinkExtractor.Entities;

namespace PodcastLinkExtractor.Data
{
    public class PodcastShowLinksContext : IShowLinksContext
    {

        private const string CONNECTION_STRING = "mongodb://linksaver:?-T96Qac@ds063406.mlab.com:63406/podcastshowlinks";
        private const string DATABASE_NAME = "podcastshowlinks";
        private const string SHOW_LINKS_COLLECTION_NAME = "showlinks";

        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static PodcastShowLinksContext()
        {
            _client = new MongoClient(CONNECTION_STRING);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        private IMongoCollection<LinkInformation> Links => _database.GetCollection<LinkInformation>(SHOW_LINKS_COLLECTION_NAME);

        public async Task AddLinks(Links links)
        {
            await Links.InsertManyAsync(links);
        }
    }
    
}
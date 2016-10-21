using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PodcastLinkExtractor.Entities
{
    public class LinkInformation
    {
        [BsonRepresentation(BsonType.String)]
        public Podcast PodcastName { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeTitle { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

    }
}
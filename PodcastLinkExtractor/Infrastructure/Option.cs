namespace PodcastLinkExtractor.Infrastructure
{
    public class Option
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Usage { get; set; }
        public SubOptions SubOptions { get; set; }
    }
}
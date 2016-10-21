using System.Collections.Generic;

namespace PodcastLinkExtractor.Infrastructure
{
    public class SubOption
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public string Usage { get; set; }
    }


    public class SubOptions : List<SubOption>
    {
        
    }
}
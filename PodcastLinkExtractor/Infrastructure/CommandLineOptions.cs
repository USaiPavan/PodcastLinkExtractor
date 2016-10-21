using System.Collections.Generic;

namespace PodcastLinkExtractor.Infrastructure
{
    public static class CommandLineOptions
    {
        public static HashSet<Option> GetAvailableOptions()
        {
            var validOptions = new HashSet<Option>();
            BuildAndAddProcessShowOptions(validOptions);
            return validOptions;
        }

        private static void BuildAndAddProcessShowOptions(HashSet<Option> validOptions)
        {
            var currentOption = new Option()
            {
                Name = Constants.PROCESS_SHOW_OPTION,
                Description = "Processes the show based on the details provided",
                Usage = ""
            };

            var currentSubOptions = new SubOptions
            {
                new SubOption()
                {   
                    Name = Constants.SHOW_OPTION,
                    Description = "Name of the show to be processed",
                    Usage = ""
                },
                new SubOption()
                {
                    Name = Constants.START_EPISODE_OPTION,
                    Description = "Start number of the episode to process",
                    Usage = ""
                },
                new SubOption()
                {
                    Name = Constants.END_EPISODE_OPTION,
                    Description = "End number of the episode to process",
                    Usage = ""
                },
                new SubOption()
                {
                    Name = Constants.SAVE_OPTION,
                    Description = "Boolean to determine if the links to be saved or printed",
                    Usage = ""
                }
            };

            currentOption.SubOptions = currentSubOptions;

            validOptions.Add(currentOption);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PodcastLinkExtractor.Entities;
using PodcastLinkExtractor.Factories;
using PodcastLinkExtractor.Infrastructure;

namespace PodcastLinkExtractor.Commands
{
    public class ProcessShowLinksCommand : Command
    {
        public ProcessShowLinksCommand(IDictionary<string, string> inputParameters) : base(inputParameters)
        {
        }


        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Execute()
        {
            if (ValidateInputParameters() == false)
            {
                return false;
            }

            Podcast podcastName;
            Enum.TryParse(InputParamters[Constants.SHOW_OPTION], out podcastName);

            var podcastInstance = LinkExtractorFactory.GetExtractorInstance(podcastName);

            var startEpisode = int.Parse(InputParamters[Constants.START_EPISODE_OPTION]);
            int? endEpisode = null;
            if (InputParamters.ContainsKey(Constants.END_EPISODE_OPTION))
            {
                endEpisode = int.Parse(InputParamters[Constants.END_EPISODE_OPTION]);
            }
            var result = await podcastInstance.GetLinks(startEpisode, endEpisode);


            if (InputParamters.ContainsKey(Constants.SAVE_OPTION))
            {
                if(bool.Parse(InputParamters[Constants.SAVE_OPTION]))
                {
                    await podcastInstance.SaveLinks(result);
                }
            }

            return true;
        }


        /// <summary>
        /// Validates the input parameters.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputParameters()
        {
            if (InputParamters.ContainsKey(Constants.SHOW_OPTION) == false)
            {
                return false;
            }

            Podcast currentPodcast;
            var result = Enum.TryParse(InputParamters[Constants.SHOW_OPTION], out currentPodcast);
            if (result == false)
            {
                return false;
            }

            if (InputParamters.ContainsKey(Constants.START_EPISODE_OPTION) == false)
            {
                return false;
            }

            int startEpisode;
            result = int.TryParse(InputParamters[Constants.START_EPISODE_OPTION], out startEpisode);
            if (result == false)
            {
                return false;
            }

            if (InputParamters.ContainsKey(Constants.END_EPISODE_OPTION))
            {
                int endEpisode;
                result = int.TryParse(InputParamters[Constants.END_EPISODE_OPTION], out endEpisode);
                if (result == false || endEpisode > startEpisode)
                {
                    return false;
                }
            }

            if (InputParamters.ContainsKey(Constants.SAVE_OPTION))
            {
                bool saveOption;
                result = bool.TryParse(InputParamters[Constants.END_EPISODE_OPTION], out saveOption);
                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
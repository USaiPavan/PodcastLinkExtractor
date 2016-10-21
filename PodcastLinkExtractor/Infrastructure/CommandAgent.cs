using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using PodcastLinkExtractor.Contracts;

namespace PodcastLinkExtractor.Infrastructure
{
    public static class CommandAgent
    {
        public static ICommand GetCommand(Option currentOption, IDictionary<string, string> inputParamters)
        {
            var resolvedInstance = Application.CurrentContainer.Resolve<ICommand>(currentOption.Name,
                new ParameterOverride("inputParameters", inputParamters));
            return resolvedInstance;
        }
    }
}
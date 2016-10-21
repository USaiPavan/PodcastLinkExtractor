using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PodcastLinkExtractor.Contracts;

namespace PodcastLinkExtractor.Commands
{
    /// <summary>
    /// Base command 
    /// </summary>
    /// <seealso cref="PodcastLinkExtractor.Contracts.ICommand" />
    public abstract class Command : ICommand
    {
        protected IDictionary<string, string> InputParamters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="inputParameters">The input parameters.</param>
        protected Command(IDictionary<string, string> inputParameters)
        {
            InputParamters = inputParameters;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public abstract Task<bool> Execute();
    }
}
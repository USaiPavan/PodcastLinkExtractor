using System;
using System.Collections.Generic;
using System.Linq;
using PodcastLinkExtractor.Commands;
using PodcastLinkExtractor.Contracts;

namespace PodcastLinkExtractor.Infrastructure
{
    public static class Parser
    {
        private static readonly HashSet<Option> _validOptions;


        static Parser()
        {
            _validOptions = CommandLineOptions.GetAvailableOptions();
        }


        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static ICommand Process(string[] args)
        {
            if (AreArgsValid(args) == false)
            {
                PrintUsage();
                return new NullCommand();
            }

            var currentOption = ParseOption(args[0]);
            var currentSubOptions = ParseSubOptions(currentOption, args);
            return CommandAgent.GetCommand(currentOption, currentSubOptions);
        }


        /// <summary>
        /// Ares the arguments valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private static bool AreArgsValid(string[] args)
        {
            var parsedOption = ParseOption(args[0]);
            if (parsedOption.GetType() == typeof(NullOption))
            {
                return false;
            }

            var parsedSubOptions = ParseSubOptions(parsedOption, args);

            if (parsedSubOptions.GetType() == typeof(NullDictionary<string,string>))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Parses the sub options.
        /// </summary>
        /// <param name="parsedOption">The parsed option.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>Dictionary containing the values or a NullDictionary indicating return of a null value</returns>
        private static IDictionary<string, string> ParseSubOptions(Option parsedOption, string[] args)
        {
            var parsedInput = new Dictionary<string, string>();
            for (int index = 1; index < args.Length; index += 2)
            {
                if (parsedOption.SubOptions.Any(so => so.Name == args[index]) == false)
                {
                    return new NullDictionary<string, string>();
                }

                parsedInput.Add(args[index], args[index + 1]);
            }
            return parsedInput;
        }


        /// <summary>
        /// Gets the option.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns></returns>
        private static Option ParseOption(string option)
        {
            var result = _validOptions.FirstOrDefault(opt => opt.Name == option);
            if (result == null)
            {
                return new NullOption();
            }
            return result;
        }

        /// <summary>
        /// Prints the usage of the commands
        /// </summary>
        private static void PrintUsage()
        {
            Console.WriteLine("Invalid paramters. Here is the usage");
        }
    }
}
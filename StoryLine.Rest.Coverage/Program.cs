using CommandLineParser.Exceptions;
using LightInject;
using StoryLine.Rest.Coverage.Services;

namespace StoryLine.Rest.Coverage
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parameters = GetCommandLineParameters(args);
            if (parameters == null)
                return;

            var container = new ServiceContainer();

            container.RegisterFrom<ServicesCompoisitionRoot>();
            ServicesCompoisitionRoot.RegisterConditionalDependencies(parameters, container);

            container.GetInstance<ICoverageCalculator>().Calculate().Wait();
        }

        /// <summary>
        /// https://github.com/j-maly/CommandLineParser/wiki
        /// </summary>
        private static CommandLineArgs GetCommandLineParameters(string[] args)
        {
            var parameters = new CommandLineArgs();

            var parser = new CommandLineParser.CommandLineParser();
            parser.ExtractArgumentAttributes(parameters);

            try
            {
                parser.ParseCommandLine(args);
            }
            catch (CommandLineException e)
            {
                parser.ShowUsageHeader = "Failed to parse command line parameters: " + e.Message;
                parser.ShowUsage();
                return null;
            }

            return parameters;
        }
    }
}

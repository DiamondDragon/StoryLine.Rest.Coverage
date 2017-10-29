using CommandLineParser.Arguments;

namespace StoryLine.Rest.Coverage
{
    public sealed class CommandLineArgs
    {
        //   [SwitchArgument('s', "swagger", true, Description = "Set whether show or not")]
        // public bool SwaggerLocation { get; set; }
        [ValueArgument(typeof(string), 's', "swagger", Description = "Swagger file to process", Optional = false)]
        public string SwaggerLocation { get; set; }

        [ValueArgument(typeof(string), 'l', "log", Description = "Log file to handle", Optional = false)]
        public string LogLocation { get; set; }

        [ValueArgument(typeof(string), 'o', "output", Description = "Location to save results", Optional = false)]
        public string OutputFilePath { get; set; }

        [EnumeratedValueArgument(typeof(string), 'f', "filter", AllowedValues = "none;service", Description = "Specifies type of filter to use")]
        public string Filter { get; set; }

        [ValueArgument(typeof(string), 'a', "argument", Description = "Filter argument to use")]
        public string FilterArgument { get; set; }

        [EnumeratedValueArgument(typeof(string), 'r', "report", AllowedValues = "notCoveredOnly;coveredOnly;all", Description = "Type of report to produce")]
        public string ReportType { get; set; }
    }
}
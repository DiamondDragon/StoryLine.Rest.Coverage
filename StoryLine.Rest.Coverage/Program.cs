using System;
using System.IO;
using CommandLineParser.Exceptions;
using LightInject;
using StoryLine.Rest.Coverage.Services;
using StoryLine.Rest.Coverage.Services.Analyzers;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;
using StoryLine.Rest.Coverage.Services.Analyzers.Matchers;
using StoryLine.Rest.Coverage.Services.Content;
using StoryLine.Rest.Coverage.Services.Factories;
using StoryLine.Rest.Coverage.Services.Parsing.Responses;
using StoryLine.Rest.Coverage.Services.Parsing.Swagger;
using StoryLine.Rest.Coverage.Services.Persisters;
using StoryLine.Rest.Coverage.Services.ResponseFilters;

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

            RegisterDependencies(container, parameters);

            container.GetInstance<ICoverageCalculator>().Calculate().Wait();
        }

        private static void RegisterDependencies(ServiceContainer container, CommandLineArgs parameters)
        {
            container.Register(x =>
                File.Exists(parameters.SwaggerLocation)
                    ? new FileContentProvider(parameters.SwaggerLocation)
                    : (ISwaggerProvider) new WebContentProvider(parameters.SwaggerLocation));
            container.Register(x =>
                File.Exists(parameters.LogLocation)
                    ? new FileContentProvider(parameters.LogLocation)
                    : (IResponseLogProvider) new WebContentProvider(parameters.LogLocation));
            container.Register<IReportPersister>(x =>
                new FilteringPersister(
                    new ReportPersister(parameters.OutputFilePath, x.GetInstance<IJsonSerializer>()),
                    p => !p.IsCovered
                ));
            container.Register(x =>
                (parameters.Filter ?? string.Empty).Equals("service", StringComparison.OrdinalIgnoreCase)
                    ? (IResponseFilter) new ServiceResponseFilter(parameters.FilterArgument)
                    : new NullResponseFilter()
            );

            container.Register<ICoverageCalculator, CoverageCalculator>();
            container.Register<ISwaggerParser, SwaggerParser>();
            container.Register<IJsonSerializer, JsonSerializer>();
            container.Register<IResponseLogParser, ResponseLogParser>();
            container.Register<IResponseLogParser, ResponseLogParser>();
            container.Register<IParameterAnalyzerFactory, ParameterAnalyzerFactory>();
            container.Register<IRequestMatcherFactory, RequestMatcherFactory>();
            container.Register<IResponseStatusCodeAnalyzerFactory, ResponseStatusCodeAnalyzerFactory>();
            container.Register<IContentAnalyzerFactory, ContentAnalyzerFactory>();
            container.Register<IPathPatternToRegexConverter, PathPatternToRegexConverter>(new PerContainerLifetime());

            container.Register<IApiAnalyzerFactory, ApiAnalyzerFactory>();
            container.Register<IOperationAnalyzerFactory, OperationAnalyzerFactory>();

            container.Register<IQueryStringParameterMatcher, QueryStringParameterMatcher>(new PerContainerLifetime());
            container.Register<IHeaderParameterMatcher, HeaderParameterMatcher>(new PerContainerLifetime());
            container.Register<IBodyParameterMatcher, BodyParameterMatcher>(new PerContainerLifetime());
            container.Register<IPathParameterMatcher, PathParameterMatcher>(new PerContainerLifetime());

            container.Register<IPathParameterMatcherFactory, PathParameterMatcherFactory>(new PerContainerLifetime());
            container.Register<IResponseContentTypeProvider, ResponseContentTypeProvider>(new PerContainerLifetime());
            container.Register<IRequestContentTypeProvider, RequestContentTypeProvider>(new PerContainerLifetime());
            container.Register<IFullUrlResolver, FullUrlResolver>(new PerContainerLifetime());
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

using System;
using System.IO;
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
            if (args.Length != 3)
            {
                Console.WriteLine("Application expests two parameters: <swagger location> <log file> <output>");
                return;
            }

            var swaggerLocation = args[0];
            var responseLogLocation = args[1];
            var outputFileName = args[2];

            var container = new ServiceContainer();

            container.Register(x => File.Exists(swaggerLocation) ? new FileContentProvider(swaggerLocation) : (ISwaggerProvider)new WebContentProvider(swaggerLocation));
            container.Register(x => File.Exists(swaggerLocation) ? new FileContentProvider(responseLogLocation) : (IResponseLogProvider)new WebContentProvider(responseLogLocation));
            container.Register<IReportPersister>(x =>
                new FilteringPersister(
                    new ReportPersister(outputFileName, x.GetInstance<IJsonSerializer>()),
                    p => !p.IsCovered
                    ));


            container.Register<IResponseFilter, NullResponseFilter>();
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

            container.GetInstance<ICoverageCalculator>().Calculate().Wait();
        }
    }
}

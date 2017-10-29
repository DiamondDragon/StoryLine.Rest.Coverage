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
    public class ServicesCompoisitionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

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

        public static void RegisterConditionalDependencies(CommandLineArgs parameters, IServiceRegistry container)
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
        }
    }
}
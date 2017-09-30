using System;
using System.Linq;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class ApiAnalyzerFactory : IApiAnalyzerFactory
    {
        private readonly IOperationAnalyzerFactory _endpointAnalyzerFactory;

        public ApiAnalyzerFactory(IOperationAnalyzerFactory endpointAnalyzerFactory)
        {
            _endpointAnalyzerFactory = endpointAnalyzerFactory ?? throw new ArgumentNullException(nameof(endpointAnalyzerFactory));
        }

        public IAnalyzer Create(ApiInfo api)
        {
            if (api == null)
                throw new ArgumentNullException(nameof(api));

            var analyzers =
                (from operation in api.Operations
                 select _endpointAnalyzerFactory.Create(operation))
                .ToArray();

            return new CompositeAnalyzer(analyzers);
        }
    }
}

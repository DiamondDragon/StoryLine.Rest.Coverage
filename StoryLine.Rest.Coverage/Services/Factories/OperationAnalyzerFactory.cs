using System;
using System.Linq;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class OperationAnalyzerFactory : IOperationAnalyzerFactory
    {
        private readonly IResponseStatusCodeAnalyzerFactory _statusCodeAnalyzerFactory;
        private readonly IContentAnalyzerFactory _contentAnalyzerFactory;
        private readonly IParameterAnalyzerFactory _parameterAnalyzerFactory;
        private readonly IRequestMatcherFactory _matcherFactory;

        public OperationAnalyzerFactory(
            IRequestMatcherFactory matcherFactory,
            IParameterAnalyzerFactory parameterAnalyzerFactory,
            IContentAnalyzerFactory contentAnalyzerFactory,
            IResponseStatusCodeAnalyzerFactory statusCodeAnalyzerFactory)
        {
            _statusCodeAnalyzerFactory = statusCodeAnalyzerFactory ?? throw new ArgumentNullException(nameof(statusCodeAnalyzerFactory));
            _contentAnalyzerFactory = contentAnalyzerFactory ?? throw new ArgumentNullException(nameof(contentAnalyzerFactory));
            _matcherFactory = matcherFactory ?? throw new ArgumentNullException(nameof(matcherFactory));
            _parameterAnalyzerFactory = parameterAnalyzerFactory ?? throw new ArgumentNullException(nameof(parameterAnalyzerFactory));
        }

        public IAnalyzer Create(OperationInfo operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            var parameterAnalyzers =
                from item in operation.Parameters
                select _parameterAnalyzerFactory.Create(operation, item);

            var requestContentAlayzers =
                from item in operation.Consumes
                select _contentAnalyzerFactory.CreateRequestContentTypeAnalyzer(operation, item);

            var responseContentAlayzers =
                from item in operation.Produces
                select _contentAnalyzerFactory.CreateResponseContentTypeAnalyzer(operation, item);

            var statusCodeAnalyzers =
                from item in operation.Responses
                select _statusCodeAnalyzerFactory.Create(operation, item);

            var innerAnalyzers =
                parameterAnalyzers
                .Concat(requestContentAlayzers)
                .Concat(responseContentAlayzers)
                .Concat(statusCodeAnalyzers)
                .ToArray();

            return new OperationAnalyzer(
                _matcherFactory.Create(operation),
                innerAnalyzers
                );
        }
    }
}
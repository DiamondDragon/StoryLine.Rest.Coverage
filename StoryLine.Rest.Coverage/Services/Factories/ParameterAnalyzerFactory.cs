using System;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class ParameterAnalyzerFactory : IParameterAnalyzerFactory
    {
        private readonly IPathParameterMatcherFactory _parameterMatcherFactory;
        private readonly IBodyParameterMatcher _bodyParameterMatcher;
        private readonly IQueryStringParameterMatcher _queryStringParameterMatcher;
        private readonly IHeaderParameterMatcher _headerParameterMatcher;

        public ParameterAnalyzerFactory(
            IHeaderParameterMatcher headerParameterMatcher,
            IQueryStringParameterMatcher queryStringParameterMatcher,
            IBodyParameterMatcher bodyParameterMatcher,
            IPathParameterMatcherFactory parameterMatcherFactory
            )
        {
            _parameterMatcherFactory = parameterMatcherFactory ?? throw new ArgumentNullException(nameof(parameterMatcherFactory));
            _bodyParameterMatcher = bodyParameterMatcher ?? throw new ArgumentNullException(nameof(bodyParameterMatcher));
            _queryStringParameterMatcher = queryStringParameterMatcher ?? throw new ArgumentNullException(nameof(queryStringParameterMatcher));
            _headerParameterMatcher = headerParameterMatcher ?? throw new ArgumentNullException(nameof(headerParameterMatcher));
        }

        public IAnalyzer Create(OperationInfo operation, ParameterInfo parameter)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            return new ParameterAnalyzer(
                operation,  
                parameter,
                _headerParameterMatcher,
                _queryStringParameterMatcher,
                _bodyParameterMatcher,
                _parameterMatcherFactory);
        }
    }
}
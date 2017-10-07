using System;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class RequestMatcherFactory : IRequestMatcherFactory
    {
        private readonly IHeaderParameterMatcher _headerParameterMatcher;
        private readonly IQueryStringParameterMatcher _queryStringParameterMatcher;
        private readonly IPathPatternToRegexConverter _pathPatternToRegexConverter;

        public RequestMatcherFactory(
            IPathPatternToRegexConverter pathPatternToRegexConverter,
            IQueryStringParameterMatcher queryStringParameterMatcher,
            IHeaderParameterMatcher headerParameterMatcher)
        {
            _headerParameterMatcher = headerParameterMatcher ?? throw new ArgumentNullException(nameof(headerParameterMatcher));
            _queryStringParameterMatcher = queryStringParameterMatcher ?? throw new ArgumentNullException(nameof(queryStringParameterMatcher));
            _pathPatternToRegexConverter = pathPatternToRegexConverter ?? throw new ArgumentNullException(nameof(pathPatternToRegexConverter));
        }

        public IOperationMatcher Create(OperationInfo operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            return new OperationMatcher(
                operation, 
                _pathPatternToRegexConverter,
                _queryStringParameterMatcher,
                _headerParameterMatcher);
        }
    }
}
using System;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;
using StoryLine.Rest.Coverage.Services.Analyzers.Matchers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class RequestMatcherFactory : IRequestMatcherFactory
    {
        private readonly IBodyParameterMatcher _bodyParameterMatcher;
        private readonly IHeaderParameterMatcher _headerParameterMatcher;
        private readonly IQueryStringParameterMatcher _queryStringParameterMatcher;
        private readonly IPathPatternToRegexConverter _pathPatternToRegexConverter;

        public RequestMatcherFactory(
            IPathPatternToRegexConverter pathPatternToRegexConverter,
            IQueryStringParameterMatcher queryStringParameterMatcher,
            IHeaderParameterMatcher headerParameterMatcher,
            IBodyParameterMatcher bodyParameterMatcher)
        {
            _bodyParameterMatcher = bodyParameterMatcher ?? throw new ArgumentNullException(nameof(bodyParameterMatcher));
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
                _headerParameterMatcher,
                _bodyParameterMatcher);
        }
    }
}
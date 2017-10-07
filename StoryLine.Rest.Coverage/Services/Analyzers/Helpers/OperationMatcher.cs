using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers.Matchers;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public class OperationMatcher : IOperationMatcher
    {
        private readonly IBodyParameterMatcher _bodyParameterMatcher;
        private readonly IHeaderParameterMatcher _headerParameterMatcher;
        private readonly IQueryStringParameterMatcher _queryStringParameterMatcher;
        private readonly OperationInfo _operation;
        private readonly RegexInfo _regexInfo;

        public OperationMatcher(
            OperationInfo operation, 
            IPathPatternToRegexConverter pathPatternToRegexConverter,
            IQueryStringParameterMatcher queryStringParameterMatcher,
            IHeaderParameterMatcher headerParameterMatcher,
            IBodyParameterMatcher bodyParameterMatcher)
        {
            if (pathPatternToRegexConverter == null)
                throw new ArgumentNullException(nameof(pathPatternToRegexConverter));

            _bodyParameterMatcher = bodyParameterMatcher ?? throw new ArgumentNullException(nameof(bodyParameterMatcher));
            _headerParameterMatcher = headerParameterMatcher ?? throw new ArgumentNullException(nameof(headerParameterMatcher));
            _queryStringParameterMatcher = queryStringParameterMatcher ?? throw new ArgumentNullException(nameof(queryStringParameterMatcher));

            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
            _regexInfo = pathPatternToRegexConverter.Convert(operation.Path);
        }

        public bool IsMatch(Request request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (!RequestMatchesHttpMethod(request.Method))
                return false;

            if (!RequestMatchesPath(request.UrlPath))
                return false;

            if (!RequestMatchesQueryParameters(request.Query))
                return false;

            if (!RequestMatchesHeaderParameters(request.Headers))
                return false;

            return RequestMatchesBodyParameters(request);
        }

        private bool RequestMatchesBodyParameters(Request request)
        {
            var hasBodyParameters = _operation.Parameters.Any(x => x.In.Equals("body") && x.Required);

            return !hasBodyParameters || _bodyParameterMatcher.HasParameter(request);
        }

        private bool RequestMatchesHttpMethod(string requestMethod)
        {
            return _operation.HttpMethod.Equals(requestMethod, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool RequestMatchesPath(string urlPath)
        {
            return _regexInfo.Pattern.Match(urlPath).Success;
        }

        private bool RequestMatchesHeaderParameters(IReadOnlyDictionary<string, string[]> requestHeaders)
        {
            return _operation.Parameters
                .Where(x => x.In.Equals("header", StringComparison.InvariantCultureIgnoreCase) && x.Required)
                .All(headerParameter => _headerParameterMatcher.HasParameter(headerParameter.Name, requestHeaders));
        }

        private bool RequestMatchesQueryParameters(IReadOnlyDictionary<string, StringValues> requestQuery)
        {
            return _operation.Parameters
                .Where(x => x.In.Equals("query", StringComparison.InvariantCultureIgnoreCase) && x.Required)
                .All(headerParameter => _queryStringParameterMatcher.HasParameter(headerParameter.Name, requestQuery));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public class OperationMatcher : IOperationMatcher
    {
        private readonly IHeaderParameterMatcher _headerParameterMatcher;
        private readonly IQueryStringParameterMatcher _queryStringParameterMatcher;
        private readonly OperationInfo _operation;
        private readonly RegexInfo _regexInfo;

        public OperationMatcher(
            OperationInfo operation, 
            IPathPatternToRegexConverter pathPatternToRegexConverter,
            IQueryStringParameterMatcher queryStringParameterMatcher,
            IHeaderParameterMatcher headerParameterMatcher)
        {
            if (pathPatternToRegexConverter == null)
                throw new ArgumentNullException(nameof(pathPatternToRegexConverter));

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

            if (!RequestMatchesBodyParameters(request.Body))
                return false;

            return true;
        }

        private bool RequestMatchesBodyParameters(byte[] requestBody)
        {
            var hasBodyParameters = _operation.Parameters.Any(x => x.In.Equals("body") && x.Required);

            if (!hasBodyParameters)
                return true;

            return requestBody?.Length > 0;
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
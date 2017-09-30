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
        private readonly OperationInfo _operation;
        private readonly RegexInfo _regexInfo;

        public OperationMatcher(OperationInfo operation, IPathPatternToRegexConverter pathPatternToRegexConverter)
        {
            if (pathPatternToRegexConverter == null)
                throw new ArgumentNullException(nameof(pathPatternToRegexConverter));

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

        private bool RequestMatchesHeaderParameters(Dictionary<string, string[]> requestHeaders)
        {
            foreach (var headerParameter in _operation.Parameters.Where(x => x.In.Equals("header", StringComparison.InvariantCultureIgnoreCase) && x.Required))
            {
                var header = requestHeaders.Keys.FirstOrDefault(x => x.Equals(headerParameter.Name, StringComparison.InvariantCultureIgnoreCase));

                if (string.IsNullOrEmpty(header))
                    return false;

                if (string.IsNullOrEmpty(requestHeaders[header].FirstOrDefault()))
                    return false;
            }

            return true;
        }

        private bool RequestMatchesQueryParameters(IDictionary<string, StringValues> requestQuery)
        {
            foreach (var headerParameter in _operation.Parameters.Where(x => x.In.Equals("header", StringComparison.InvariantCultureIgnoreCase) && x.Required))
            {
                var header = requestQuery.Keys.FirstOrDefault(x => x.Equals(headerParameter.Name, StringComparison.InvariantCultureIgnoreCase));

                if (string.IsNullOrEmpty(header))
                    return false;

                if (string.IsNullOrEmpty(requestQuery[header].FirstOrDefault()))
                    return false;
            }

            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public class QueryStringParameterMatcher : IQueryStringParameterMatcher
    {
        public bool HasParameter(string parameterName, IReadOnlyDictionary<string, StringValues> queryString)
        {
            if (queryString == null)
                throw new ArgumentNullException(nameof(queryString));
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameterName));

            var header = queryString.Keys.FirstOrDefault(x => x.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(header))
                return false;

            return !string.IsNullOrEmpty(queryString[header].FirstOrDefault());
        }
    }
}
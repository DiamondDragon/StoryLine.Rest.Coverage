using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Matchers
{
    public class HeaderParameterMatcher : IHeaderParameterMatcher
    {
        public bool HasParameter(string parameterName, IReadOnlyDictionary<string, string[]> requestHeaders)
        {
            var header = requestHeaders.Keys.FirstOrDefault(x => x.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(header))
                return false;

            return !string.IsNullOrEmpty(requestHeaders[header].FirstOrDefault());
        }
    }
}
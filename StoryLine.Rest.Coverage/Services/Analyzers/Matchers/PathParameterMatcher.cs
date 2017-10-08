using System;
using System.Linq;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Matchers
{
    public class PathParameterMatcher : IPathParameterMatcher
    {
        private readonly RegexInfo _regexInfo;

        public PathParameterMatcher(RegexInfo regexInfo)
        {
            _regexInfo = regexInfo ?? throw new ArgumentNullException(nameof(regexInfo));
        }

        public bool HasParameter(string parameterName, string path)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameterName));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            var mapping = _regexInfo.GroupToParamMapping.FirstOrDefault(x => x.Value.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(mapping.Key))
                return false;

            var match = _regexInfo.Pattern.Match(path);
            if (!match.Success)
                return false;

            return !string.IsNullOrEmpty(match.Groups[mapping.Key].Value);
        }
    }
}
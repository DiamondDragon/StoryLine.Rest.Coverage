using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public class PathPatternToRegexConverter : IPathPatternToRegexConverter
    {
        private static readonly Regex ParameterPattern = new Regex(@"(?<parameterName>\{[^}]+\})", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public RegexInfo Convert(string pathPattern)
        {
            if (string.IsNullOrWhiteSpace(pathPattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pathPattern));

            var parameterMap = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            var pattern = ParameterPattern.Replace(pathPattern, x => OnResultFound(parameterMap, x));

            return new RegexInfo(
                new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                parameterMap
                );
        }

        private static string OnResultFound(IDictionary<string, string> parameterMap, Capture match)
        {
            var parameterName = "p" + parameterMap.Count;

            parameterMap.Add(parameterName, match.Value.Substring(1, match.Value.Length - 2));

            return $"(?<{parameterName}>[^\\/]+)";
        }
    }
}
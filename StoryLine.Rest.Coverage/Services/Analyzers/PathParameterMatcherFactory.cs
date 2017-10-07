using System;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class PathParameterMatcherFactory : IPathParameterMatcherFactory
    {
        private readonly IPathPatternToRegexConverter _regexConverter;

        public PathParameterMatcherFactory(IPathPatternToRegexConverter regexConverter)
        {
            _regexConverter = regexConverter ?? throw new ArgumentNullException(nameof(regexConverter));
        }

        public IPathParameterMatcher Create(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

            return new PathParameterMatcher(_regexConverter.Convert(path));
        }
    }
}
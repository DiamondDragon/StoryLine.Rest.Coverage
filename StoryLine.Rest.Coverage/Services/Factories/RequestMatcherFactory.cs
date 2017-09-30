using System;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class RequestMatcherFactory : IRequestMatcherFactory
    {
        private readonly IPathPatternToRegexConverter _pathPatternToRegexConverter;

        public RequestMatcherFactory(IPathPatternToRegexConverter pathPatternToRegexConverter)
        {
            _pathPatternToRegexConverter = pathPatternToRegexConverter ?? throw new ArgumentNullException(nameof(pathPatternToRegexConverter));
        }

        public IOperationMatcher Create(OperationInfo operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            return new OperationMatcher(operation, _pathPatternToRegexConverter);
        }
    }
}
using System.Collections.Generic;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Matchers
{
    public interface IHeaderParameterMatcher
    {
        bool HasParameter(string parameterName, IReadOnlyDictionary<string, string[]> requestHeaders);
    }
}
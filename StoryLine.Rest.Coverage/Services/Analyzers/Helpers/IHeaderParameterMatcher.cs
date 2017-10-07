using System.Collections.Generic;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public interface IHeaderParameterMatcher
    {
        bool HasParameter(string parameterName, IReadOnlyDictionary<string, string[]> requestHeaders);
    }
}
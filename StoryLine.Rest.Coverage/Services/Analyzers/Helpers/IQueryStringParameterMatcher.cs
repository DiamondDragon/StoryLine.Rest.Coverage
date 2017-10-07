using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public interface IQueryStringParameterMatcher
    {
        bool HasParameter(string parameterName, IReadOnlyDictionary<string, StringValues> queryString);
    }
}
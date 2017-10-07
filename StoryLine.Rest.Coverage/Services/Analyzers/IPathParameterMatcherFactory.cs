using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;
using StoryLine.Rest.Coverage.Services.Analyzers.Matchers;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public interface IPathParameterMatcherFactory
    {
        IPathParameterMatcher Create(string path);
    }
}
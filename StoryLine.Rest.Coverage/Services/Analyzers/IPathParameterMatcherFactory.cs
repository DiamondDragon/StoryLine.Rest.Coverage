using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public interface IPathParameterMatcherFactory
    {
        IPathParameterMatcher Create(string path);
    }
}
namespace StoryLine.Rest.Coverage.Services.Analyzers.Matchers
{
    public interface IPathParameterMatcherFactory
    {
        IPathParameterMatcher Create(string path);
    }
}
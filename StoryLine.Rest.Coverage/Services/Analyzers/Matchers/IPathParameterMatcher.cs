namespace StoryLine.Rest.Coverage.Services.Analyzers.Matchers
{
    public interface IPathParameterMatcher
    {
        bool HasParameter(string parameterName, string path);
    }
}
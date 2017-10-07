namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public interface IPathParameterMatcher
    {
        bool HasParameter(string parameterName, string path);
    }
}
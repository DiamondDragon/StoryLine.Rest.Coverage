namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public interface IPathPatternToRegexConverter
    {
        RegexInfo Convert(string pathPattern);
    }
}
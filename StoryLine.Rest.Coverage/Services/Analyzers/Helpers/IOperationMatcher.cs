using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public interface IOperationMatcher
    {
        bool IsMatch(Request request);
    }
}
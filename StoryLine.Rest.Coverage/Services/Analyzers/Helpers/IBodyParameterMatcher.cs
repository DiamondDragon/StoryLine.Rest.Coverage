using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public interface IBodyParameterMatcher
    {
        bool HasParameter(Request request);
    }
}
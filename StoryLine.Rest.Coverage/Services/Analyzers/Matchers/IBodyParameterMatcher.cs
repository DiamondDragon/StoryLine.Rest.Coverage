using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Matchers
{
    public interface IBodyParameterMatcher
    {
        bool HasParameter(Request request);
    }
}
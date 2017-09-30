using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public interface IApiAnalyzerFactory
    {
        IAnalyzer Create(ApiInfo api);
    }
}
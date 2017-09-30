using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public interface IContentAnalyzerFactory
    {
        IAnalyzer CreateResponseContentTypeAnalyzer(OperationInfo operation, string contentType);
        IAnalyzer CreateRequestContentTypeAnalyzer(OperationInfo operation, string contentType);
    }
}
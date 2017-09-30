using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class ContentAnalyzerFactory : IContentAnalyzerFactory
    {
        public IAnalyzer CreateResponseContentTypeAnalyzer(OperationInfo operation, string contentType)
        {
            return new ResponseContentTypeAnalyzer(operation, contentType);
        }

        public IAnalyzer CreateRequestContentTypeAnalyzer(OperationInfo operation, string contentType)
        {
            return new RequestContentTypeAnalyzer(operation, contentType);
        }
    }
}
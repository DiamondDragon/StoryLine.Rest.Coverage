using System;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class ContentAnalyzerFactory : IContentAnalyzerFactory
    {
        private readonly IResponseContentTypeProvider _responseContentTypeProvider;
        private readonly IRequestContentTypeProvider _requestContentTypeProvider;

        public ContentAnalyzerFactory(
            IRequestContentTypeProvider requestContentTypeProvider,
            IResponseContentTypeProvider responseContentTypeProvider)
        {
            _responseContentTypeProvider = responseContentTypeProvider ?? throw new ArgumentNullException(nameof(responseContentTypeProvider));
            _requestContentTypeProvider = requestContentTypeProvider ?? throw new ArgumentNullException(nameof(requestContentTypeProvider));
        }

        public IAnalyzer CreateResponseContentTypeAnalyzer(OperationInfo operation, string contentType)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(contentType));

            return new ResponseContentTypeAnalyzer(operation, contentType, _responseContentTypeProvider);
        }

        public IAnalyzer CreateRequestContentTypeAnalyzer(OperationInfo operation, string contentType)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(contentType));

            return new RequestContentTypeAnalyzer(operation, contentType, _requestContentTypeProvider);
        }
    }
}
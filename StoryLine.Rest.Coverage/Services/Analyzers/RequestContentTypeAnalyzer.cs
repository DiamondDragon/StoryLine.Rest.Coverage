using System;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class RequestContentTypeAnalyzer : IAnalyzer
    {
        private readonly OperationInfo _operation;
        private readonly string _contentType;

        public RequestContentTypeAnalyzer(OperationInfo operation, string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(contentType));

            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
            _contentType = contentType;
        }

        public void Process(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            // TODO: Handle request content type
        }

        public IAnalysisReport GetReport()
        {
            return new AnalysisReport
            {
                Operation = _operation
            };
        }
    }
}
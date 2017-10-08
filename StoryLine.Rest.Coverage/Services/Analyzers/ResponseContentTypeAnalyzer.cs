using System;
using System.Collections.Generic;
using System.Linq;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class ResponseContentTypeAnalyzer : IAnalyzer
    {
        private readonly OperationInfo _operation;
        private readonly string _contentType;
        private readonly IResponseContentTypeProvider _responseContentTypeProvider;

        private readonly List<Response> _matchingResponses = new List<Response>();

        public ResponseContentTypeAnalyzer(OperationInfo operation, string contentType, IResponseContentTypeProvider responseContentTypeProvider)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(contentType));

            _responseContentTypeProvider = responseContentTypeProvider ?? throw new ArgumentNullException(nameof(responseContentTypeProvider));
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
            _contentType = contentType;
        }

        public void Process(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            var contentType = _responseContentTypeProvider.GetContentType(response.Headers);

            if (string.IsNullOrEmpty(contentType))
                return;

            if (_contentType.Equals(contentType, StringComparison.InvariantCultureIgnoreCase))
                _matchingResponses.Add(response);
        }

        public IEnumerable<IAnalysisReport> GetReports()
        {
            yield return new AnalysisReport
            {
                OperationId = _operation.OperationdId,
                Path = _operation.Path,
                HttpMethod = _operation.HttpMethod,
                IsMandatoryCase = true,
                AnalyzerId = nameof(ResponseContentTypeAnalyzer),
                IsCovered = _matchingResponses.Count > 0,
                AnalyzedCase = $"Response \"Content-Type\" header equal to \"{_contentType}\"",
                MatchingResponse = _matchingResponses
            };
        }
    }
}
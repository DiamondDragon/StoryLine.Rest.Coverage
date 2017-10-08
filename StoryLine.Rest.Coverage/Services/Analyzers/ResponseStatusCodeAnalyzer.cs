using System;
using System.Collections.Generic;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class ResponseStatusCodeAnalyzer : IAnalyzer
    {
        private readonly OperationInfo _operation;
        private readonly int _statusCode;
        private readonly List<Response> _matchingRespones = new List<Response>();

        public ResponseStatusCodeAnalyzer(OperationInfo operation, int statusCode)
        {
            if (statusCode <= 0)
                throw new ArgumentOutOfRangeException(nameof(statusCode));

            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
            _statusCode = statusCode;
        }

        public void Process(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (response.Status == _statusCode)
                _matchingRespones.Add(response);
        }

        public IEnumerable<IAnalysisReport> GetReports()
        {
            yield return new AnalysisReport
            {
                OperationId = _operation.OperationdId,
                Path = _operation.Path,
                HttpMethod = _operation.HttpMethod,
                IsMandatoryCase = true,
                AnalyzerId = nameof(ResponseStatusCodeAnalyzer),
                IsCovered = _matchingRespones.Count > 0,
                AnalyzedCase = $"Response HTTP status code equal to {_statusCode}",
                MatchingResponse = _matchingRespones
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IAnalysisReport GetReport()
        {
            return new AnalysisReport
            {
                Operation = _operation.OperationdId,
                TotalCount = 1,
                CoveredCount = _matchingRespones.Count > 0 ? 1 : 0,
                Errors = _matchingRespones.Count > 0 ? Enumerable.Empty<Error>() : new [] { CreateError() }
            };
        }

        private Error CreateError()
        {
            return new Error
            {
                Id = "StatusCode",
                Message = $"Neither of recorded tests produces response with status code {_statusCode}."
            };
        }
    }
}
﻿using System;
using System.Collections.Generic;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class RequestContentTypeAnalyzer : IAnalyzer
    {
        private readonly OperationInfo _operation;
        private readonly string _contentType;
        private readonly IRequestContentTypeProvider _responseContentTypeProvider;

        private readonly List<Response> _matchingResponses = new List<Response>();

        public RequestContentTypeAnalyzer(OperationInfo operation, string contentType, IRequestContentTypeProvider requestContentTypeProvider)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(contentType));

            _responseContentTypeProvider = requestContentTypeProvider ?? throw new ArgumentNullException(nameof(requestContentTypeProvider));
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));

            _contentType = contentType;
        }

        public void Process(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            var contentType = _responseContentTypeProvider.GetContentType(response.Request.Headers);

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
                AnalyzerId = nameof(RequestContentTypeAnalyzer),
                IsCovered = _matchingResponses.Count > 0,
                AnalyzedCase = $"Request \"Accept\" header equal to \"{_contentType}\"",
                MatchingResponse = _matchingResponses
            };
        }
    }
}
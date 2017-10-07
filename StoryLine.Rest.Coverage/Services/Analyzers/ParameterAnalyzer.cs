using System;
using System.Collections.Generic;
using System.Linq;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;
using StoryLine.Rest.Coverage.Services.Analyzers.Matchers;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class ParameterAnalyzer : IAnalyzer
    {
        private static readonly Dictionary<string, Func<ParameterAnalyzer, Request, bool>> ParamTypePredicates = new Dictionary<string, Func<ParameterAnalyzer, Request, bool>>(StringComparer.InvariantCultureIgnoreCase)
        {
            ["body"] = (a, r) => a._bodyParameterMatcher.HasParameter(r),
            ["query"] = (a, r) => a._queryStringParameterMatcher.HasParameter(a._parameter.Name, r.Query),
            ["path"] = (a, r) => a.PathParameterMatcher.HasParameter(a._parameter.Name, r.UrlPath),
            ["header"] = (a, r) => a._headerParameterMatcher.HasParameter(a._parameter.Name, r.Headers)
        };

        private readonly IPathParameterMatcherFactory _pathParameterMatcherFactory;
        private readonly IBodyParameterMatcher _bodyParameterMatcher;
        private readonly IQueryStringParameterMatcher _queryStringParameterMatcher;
        private readonly IHeaderParameterMatcher _headerParameterMatcher;
        private readonly ParameterInfo _parameter;
        private readonly OperationInfo _operation;

        private IPathParameterMatcher _pathParameterMatcher;

        private IPathParameterMatcher PathParameterMatcher
        {
            get
            {
                if (_pathParameterMatcher == null)
                    _pathParameterMatcher = _pathParameterMatcherFactory.Create(_operation.Path);

                return _pathParameterMatcher;
            }
        }

        private readonly List<Response> _matchingRequests = new List<Response>();

        public ParameterAnalyzer(
            OperationInfo opetionInfo,
            ParameterInfo parameter,
            IHeaderParameterMatcher headerParameterMatcher,
            IQueryStringParameterMatcher queryStringParameterMatcher,
            IBodyParameterMatcher bodyParameterMatcher,
            IPathParameterMatcherFactory pathParameterMatcherFactory)
        {
            _pathParameterMatcherFactory = pathParameterMatcherFactory ?? throw new ArgumentNullException(nameof(pathParameterMatcherFactory));
            _bodyParameterMatcher = bodyParameterMatcher ?? throw new ArgumentNullException(nameof(bodyParameterMatcher));
            _queryStringParameterMatcher = queryStringParameterMatcher ?? throw new ArgumentNullException(nameof(queryStringParameterMatcher));
            _headerParameterMatcher = headerParameterMatcher ?? throw new ArgumentNullException(nameof(headerParameterMatcher));
            _operation = opetionInfo ?? throw new ArgumentNullException(nameof(opetionInfo));
            _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

        public void Process(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (string.IsNullOrEmpty(_parameter.In))
                return;

            if (!ParamTypePredicates.ContainsKey(_parameter.In))
                return;

            if (ParamTypePredicates[_parameter.In](this, response.Request))
                _matchingRequests.Add(response);
        }

        public IAnalysisReport GetReport()
        {
            return new AnalysisReport
            {
                Operation = _operation.OperationdId,
                CoveredCount = _matchingRequests.Count > 0 ? 1 : 0,
                TotalCount = 1,
                Errors = _matchingRequests.Count > 0 ? Enumerable.Empty<Error>() : new[] { new Error
                {
                    Id = "ParameterAnalysis",
                    Message = $"No requests use parameter \"{_parameter.Name}\" of type \"{_parameter.In}\"" }
                } 
            };
        }
    }
}
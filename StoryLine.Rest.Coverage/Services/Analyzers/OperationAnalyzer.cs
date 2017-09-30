using System;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class OperationAnalyzer : CompositeAnalyzer
    {
        private readonly IOperationMatcher _operationMatcher;

        public OperationAnalyzer(IOperationMatcher operationMatcher, params IAnalyzer[] analyzers)
            : base(analyzers)
        {
            _operationMatcher = operationMatcher ?? throw new ArgumentNullException(nameof(operationMatcher));
        }

        public override void Process(Response response)
        {
            if (!_operationMatcher.IsMatch(response.Request))
                return;



            base.Process(response);
        }
    }
}
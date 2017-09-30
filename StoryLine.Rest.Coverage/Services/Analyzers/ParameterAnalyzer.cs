using System;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class ParameterAnalyzer : IAnalyzer
    {
        private readonly ParameterInfo _parameter;
        private readonly OperationInfo _operation;

        public ParameterAnalyzer(
            OperationInfo opetionInfo,
            ParameterInfo parameter)
        {
            _operation = opetionInfo ?? throw new ArgumentNullException(nameof(opetionInfo));
            _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

        public void Process(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            // TODO: Validate parameter
        }

        public IAnalysisReport GetReport()
        {
            return new AnalysisReport
            {
                Operation = _operation,
                // TODO: Report results
            };
        }
    }
}
using System;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class ParameterAnalyzerFactory : IParameterAnalyzerFactory
    {
        public IAnalyzer Create(OperationInfo operation, ParameterInfo parameter)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            return new ParameterAnalyzer(operation,  parameter);
        }
    }
}
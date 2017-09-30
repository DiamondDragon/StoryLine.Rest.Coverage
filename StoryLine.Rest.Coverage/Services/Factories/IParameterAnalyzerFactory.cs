using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public interface IParameterAnalyzerFactory
    {
        IAnalyzer Create(OperationInfo operation, ParameterInfo parameter);
    }
}
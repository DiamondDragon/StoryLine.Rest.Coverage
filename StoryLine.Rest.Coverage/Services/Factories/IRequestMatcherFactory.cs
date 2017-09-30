using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public interface IRequestMatcherFactory
    {
        IOperationMatcher Create(OperationInfo operation);
    }
}
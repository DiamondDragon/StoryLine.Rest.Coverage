using System;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Factories
{
    public class ResponseStatusCodeAnalyzerFactory : IResponseStatusCodeAnalyzerFactory
    {
        public IAnalyzer Create(OperationInfo operation, ResponseInfo response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            return new ResponseStatusCodeAnalyzer(operation, response.StatusCode);
        }
    }
}
using System;
using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public class BodyParameterMatcher : IBodyParameterMatcher
    {
        public bool HasParameter(Request request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return request.Body?.Length > 0;
        }
    }
}
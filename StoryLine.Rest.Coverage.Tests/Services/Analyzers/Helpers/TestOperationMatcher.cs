using StoryLine.Rest.Coverage.Model.Response;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;
using StoryLine.Rest.Coverage.Services.Analyzers.Matchers;
using Xunit;

namespace StoryLine.Rest.Coverage.Tests.Services.Analyzers.Helpers
{
    public class TestOperationMatcher
    {
        private readonly OperationMatcher _underTest;
        private OperationInfo _operation;

        public TestOperationMatcher()
        {
            _operation = new OperationInfo
            {
                Path = "/v1/clients/{clientId}/plans/{planId}"
            };

            _underTest = new OperationMatcher(
                _operation, 
                new PathPatternToRegexConverter(),
                new QueryStringParameterMatcher(), 
                new HeaderParameterMatcher(),
                new BodyParameterMatcher());
        }

        [Fact]
        public void Test()
        {
            var request = new Request
            {
                Url = "/v1/clients/123/plans/234?tenantId=123&user=444"
            };


            _underTest.IsMatch(request);
        }
    }
}

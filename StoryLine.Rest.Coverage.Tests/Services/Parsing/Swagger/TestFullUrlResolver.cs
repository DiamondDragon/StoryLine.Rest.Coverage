using FluentAssertions;
using StoryLine.Rest.Coverage.Services.Parsing.Swagger;
using Xunit;

namespace StoryLine.Rest.Coverage.Tests.Services.Parsing.Swagger
{
    public class TestFullUrlResolver
    {
        private readonly FullUrlResolver _underTest;

        public TestFullUrlResolver()
        {
            _underTest = new FullUrlResolver();
        }

        [Theory]
        [InlineData("/", "/v1/dragon", "/v1/dragon")]
        [InlineData("/v1", "/dragon", "/v1/dragon")]
        [InlineData("/v1", "dragon", "/v1/dragon")]
        [InlineData("/v1/", "dragon", "/v1/dragon")]
        [InlineData("", "v1/dragon", "v1/dragon")]
        [InlineData(null, "v1/dragon", "v1/dragon")]
        public void Resolve_Should_Return_ExpectedResult(string basePath, string path, string expectedPath)
        {
            _underTest.Resolve(basePath, path).Should().Be(expectedPath);
        }
    }
}

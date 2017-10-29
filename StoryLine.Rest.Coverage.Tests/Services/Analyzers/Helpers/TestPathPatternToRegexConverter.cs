using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentAssertions;
using StoryLine.Rest.Coverage.Services.Analyzers.Helpers;
using Xunit;

namespace StoryLine.Rest.Coverage.Tests.Services.Analyzers.Helpers
{
    public class TestPathPatternToRegexConverter
    {
        private readonly PathPatternToRegexConverter _underTest;

        public TestPathPatternToRegexConverter()
        {
            _underTest = new PathPatternToRegexConverter();
        }

        [Fact]
        public void Convert_When_One_Parameter_Should_Return_Expected_Result()
        {
            _underTest.Convert("/v1/clients/{clientId}").ShouldBeEquivalentTo(
                new RegexInfo(
                    new Regex("/v1/clients/(?<p0>[^\\/]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase),
                    new Dictionary<string, string>
                    {
                        ["p0"] = "clientId"
                    }));
        }

        [Fact]
        public void Convert_When_Multiple_Parameters_Should_Return_Expected_Result()
        {
            _underTest.Convert("/v1/clients/{clientId}/plans/{planId}/subplans/{subPlanId}").ShouldBeEquivalentTo(
                new RegexInfo(
                    new Regex("/v1/clients/(?<p0>[^\\/]+)/plans/(?<p1>[^\\/]+)/subplans/(?<p2>[^\\/]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase),
                    new Dictionary<string, string>
                    {
                        ["p0"] = "clientId",
                        ["p1"] = "planId",
                        ["p2"] = "subPlanId"
                    }));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class AnalysisReport : IAnalysisReport
    {
        public string Operation { get; set; }

        public int TotalCount { get; set; }
        public int CoveredCount { get; set; }
        public IEnumerable<Error> Errors { get; set; } = Enumerable.Empty<Error>();
    }
}
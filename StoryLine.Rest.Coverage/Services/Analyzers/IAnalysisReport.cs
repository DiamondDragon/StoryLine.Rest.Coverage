using System.Collections.Generic;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public interface IAnalysisReport
    {
        string Operation { get; }
        int TotalCount { get; }
        int CoveredCount { get; }
        IEnumerable<Error> Errors { get; }
    }
}
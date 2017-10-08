using System.Collections.Generic;
using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public interface IAnalysisReport
    {
        string OperationId { get; }
        string HttpMethod { get; }
        string Path { get; }

        string AnalyzerId { get; }
        string AnalyzedCase { get; }
        bool IsMandatoryCase { get; }
        bool IsCovered { get; }

        IEnumerable<Response> MatchingResponse { get; }
    }
}
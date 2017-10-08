using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class AnalysisReport : IAnalysisReport
    {
        public string OperationId { get; set; }
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public string AnalyzerId { get; set; }
        public string AnalyzedCase { get; set; }
        public bool IsMandatoryCase { get; set; }
        public bool IsCovered { get; set; }

        [JsonIgnore]
        public IEnumerable<Response> MatchingResponse { get; set; } = Enumerable.Empty<Response>();
    }
}
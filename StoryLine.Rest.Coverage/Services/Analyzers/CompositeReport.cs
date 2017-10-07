using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class CompositeReport : IAnalysisReport
    {
        [JsonIgnore]
        public IAnalysisReport[] Reports { get; }

        public CompositeReport(params IAnalysisReport[] reports)
        {
            Reports = reports ?? throw new ArgumentNullException(nameof(reports));
        }

        public string Operation => Reports[0].Operation;
        public int TotalCount => Reports.Sum(x => x.TotalCount);
        public int CoveredCount => Reports.Sum(x => x.CoveredCount);
        public IEnumerable<Error> Errors => Reports.SelectMany(x => x.Errors);
    }
}
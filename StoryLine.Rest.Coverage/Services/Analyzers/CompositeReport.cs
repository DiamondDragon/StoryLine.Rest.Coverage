using System;
using System.Collections.Generic;
using System.Linq;
using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class CompositeReport : IAnalysisReport
    {
        private readonly IAnalysisReport[] _reports;

        public CompositeReport(params IAnalysisReport[] reports)
        {
            _reports = reports ?? throw new ArgumentNullException(nameof(reports));
        }

        public OperationInfo Operation => _reports[0].Operation;
        public int TotalCount => _reports.Sum(x => x.TotalCount);
        public int CoveredCount => _reports.Sum(x => x.CoveredCount);
        public IEnumerable<Error> Errors => _reports.SelectMany(x => x.Errors);
    }
}
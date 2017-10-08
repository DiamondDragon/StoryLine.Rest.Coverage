using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Persisters
{
    public class FilteringPersister : IReportPersister
    {
        private readonly Func<IAnalysisReport, bool> _filter;
        private readonly IReportPersister _innerPersister;

        public FilteringPersister(IReportPersister innerAnalyzer, Func<IAnalysisReport, bool> filter)
        {
            _innerPersister = innerAnalyzer ?? throw new ArgumentNullException(nameof(innerAnalyzer));
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public async Task Save(IEnumerable<IAnalysisReport> reports)
        {
            if (reports == null)
                throw new ArgumentNullException(nameof(reports));

            await _innerPersister.Save(reports.Where(_filter));
        }
    }
}
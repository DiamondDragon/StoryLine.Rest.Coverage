using System;
using System.Collections.Generic;
using System.Linq;
using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public class CompositeAnalyzer : IAnalyzer
    {
        public IAnalyzer[] Analyzers { get; }

        public CompositeAnalyzer(params IAnalyzer[] analyzers)
        {
            Analyzers = analyzers ?? throw new ArgumentNullException(nameof(analyzers));
        }

        public virtual void Process(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            Array.ForEach(Analyzers, x => x.Process(response));
        }

        public IEnumerable<IAnalysisReport> GetReports()
        {
            return Analyzers.SelectMany(x => x.GetReports());
        }
    }
}
using System.Collections.Generic;
using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public interface IAnalyzer
    {
        void Process(Response response);
        IEnumerable<IAnalysisReport> GetReports();
    }
}
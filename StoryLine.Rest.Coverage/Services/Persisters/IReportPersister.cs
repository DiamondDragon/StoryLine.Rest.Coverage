using System.Collections.Generic;
using System.Threading.Tasks;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services.Persisters
{
    public interface IReportPersister
    {
        Task Save(IEnumerable<IAnalysisReport> reports);
    }
}
using System.Threading.Tasks;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services
{
    public interface IReportPersister
    {
        Task Save(IAnalysisReport report);
    }
}
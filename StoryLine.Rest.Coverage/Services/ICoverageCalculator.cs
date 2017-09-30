using System.Threading.Tasks;

namespace StoryLine.Rest.Coverage.Services
{
    public interface ICoverageCalculator
    {
        Task Calculate();
    }
}
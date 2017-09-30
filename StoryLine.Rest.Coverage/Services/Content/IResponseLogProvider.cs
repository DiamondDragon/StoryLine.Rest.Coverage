using System.Threading.Tasks;

namespace StoryLine.Rest.Coverage.Services.Content
{
    public interface IResponseLogProvider
    {
        Task<string> GetContent();
    }
}

using System.Threading.Tasks;

namespace StoryLine.Rest.Coverage.Services.Content
{
    public interface ISwaggerProvider
    {
        Task<string> GetContent();
    }
}
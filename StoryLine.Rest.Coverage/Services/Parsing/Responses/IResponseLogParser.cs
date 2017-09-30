using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Parsing.Responses
{
    public interface IResponseLogParser
    {
        ResponseLog Parse(string content);
    }
}

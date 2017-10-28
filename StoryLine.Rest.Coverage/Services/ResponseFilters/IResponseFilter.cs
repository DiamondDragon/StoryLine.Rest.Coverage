using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.ResponseFilters
{
    public interface IResponseFilter
    {
        bool IsValid(Response response);
    }
}
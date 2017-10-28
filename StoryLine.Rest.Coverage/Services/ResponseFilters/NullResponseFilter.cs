using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.ResponseFilters
{
    public class NullResponseFilter : IResponseFilter
    {
        public bool IsValid(Response response) => true;
    }
}
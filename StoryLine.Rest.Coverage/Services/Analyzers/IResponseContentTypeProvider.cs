using System.Collections.Generic;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public interface IResponseContentTypeProvider
    {
        string GetContentType(IEnumerable<KeyValuePair<string, string[]>> headers);
    }
}
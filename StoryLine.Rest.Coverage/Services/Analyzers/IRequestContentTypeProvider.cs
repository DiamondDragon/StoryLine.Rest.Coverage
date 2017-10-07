using System.Collections.Generic;

namespace StoryLine.Rest.Coverage.Services.Analyzers
{
    public interface IRequestContentTypeProvider
    {
        string GetContentType(IEnumerable<KeyValuePair<string, string[]>> headers);
    }
}
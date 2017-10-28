namespace StoryLine.Rest.Coverage.Services.Parsing.Swagger
{
    public class FullUrlResolver : IFullUrlResolver
    {
        public string Resolve(string basePath, string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                return relativeUrl ?? string.Empty;

            if (basePath.EndsWith("/"))
                basePath = basePath.Substring(0, basePath.Length - 1);

            if (!relativeUrl.StartsWith("/"))
                relativeUrl = "/" + relativeUrl;

            return basePath + relativeUrl;
        }
    }
}
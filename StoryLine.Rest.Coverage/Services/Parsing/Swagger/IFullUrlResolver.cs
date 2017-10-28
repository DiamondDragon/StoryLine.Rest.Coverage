namespace StoryLine.Rest.Coverage.Services.Parsing.Swagger
{
    public interface IFullUrlResolver
    {
        string Resolve(string basePath, string relativeUrl);
    }
}
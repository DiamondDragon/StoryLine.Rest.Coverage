using StoryLine.Rest.Coverage.Model.Swagger;

namespace StoryLine.Rest.Coverage.Services.Parsing.Swagger
{
    public interface ISwaggerParser
    {
        ApiInfo Parse(string swaggerJson);
    }
}
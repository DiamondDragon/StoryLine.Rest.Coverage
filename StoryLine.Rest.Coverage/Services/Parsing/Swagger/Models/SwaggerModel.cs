using System.Collections.Generic;

namespace StoryLine.Rest.Coverage.Services.Parsing.Swagger.Models
{
    public class SwaggerModel
    {
        public string BasePath { get; set; }
        public Dictionary<string, Dictionary<string, OperationModel>> Paths { get; set; } = new Dictionary<string, Dictionary<string, OperationModel>>();
    }
}
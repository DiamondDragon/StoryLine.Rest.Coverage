namespace StoryLine.Rest.Coverage.Services.Parsing.Swagger.Models
{
    public class ParameterModel
    {
        public string Name { get; set; }
        public string In { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
    }
}
namespace StoryLine.Rest.Coverage.Model.Swagger
{
    public class OperationInfo
    {
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public string OperationdId { get; set; }
        public string[] Consumes { get; set; } = new string[0];
        public string[] Produces { get; set; } = new string[0];
        public ParameterInfo[] Parameters = new ParameterInfo[0];
        public ResponseInfo[] Responses = new ResponseInfo[0];
    }
}
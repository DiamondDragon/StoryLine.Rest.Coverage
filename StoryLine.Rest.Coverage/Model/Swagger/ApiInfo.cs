namespace StoryLine.Rest.Coverage.Model.Swagger
{
    public class ApiInfo
    {
        public string BasePath { get; set; } = string.Empty;

        public OperationInfo[] Operations = new OperationInfo[0];
    }
}

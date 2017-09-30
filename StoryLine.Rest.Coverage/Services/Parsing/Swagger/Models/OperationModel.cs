using System.Collections.Generic;

namespace StoryLine.Rest.Coverage.Services.Parsing.Swagger.Models
{
    public class OperationModel
    {
        public string OperationId { get; set; }
        public string[] Consumes { get; set; } = new string[0];
        public string[] Produces { get; set; } = new string[0];
        public ParameterModel[] Parameters { get; set; } = new ParameterModel[0];
        public Dictionary<int, ResponseModel> Responses { get; set; } = new Dictionary<int, ResponseModel>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using StoryLine.Rest.Coverage.Exceptions;
using StoryLine.Rest.Coverage.Model.Swagger;
using StoryLine.Rest.Coverage.Services.Parsing.Swagger.Models;

namespace StoryLine.Rest.Coverage.Services.Parsing.Swagger
{
    public class SwaggerParser : ISwaggerParser
    {
        private readonly IJsonSerializer _jsonSerializer;

        public SwaggerParser(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
        }

        public ApiInfo Parse(string swaggerJson)
        {
            if (string.IsNullOrWhiteSpace(swaggerJson))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(swaggerJson));

            var model = DeserializeModel(swaggerJson);

            return new ApiInfo
            {
                BasePath = model.BasePath ?? string.Empty,
                Operations = GetOperationList(model)
            };
        }

        private static OperationInfo[] GetOperationList(SwaggerModel model)
        {
            return 
                (from path in model.Paths
                 from operation in path.Value
                 select GetOperation(path.Key, operation.Key, operation.Value))
                .ToArray();
        }

        private static OperationInfo GetOperation(string path, string httpMethod, OperationModel operation)
        {
            return new OperationInfo
            {
                HttpMethod = httpMethod,
                Path = path,
                Consumes = operation.Consumes,
                OperationdId = operation.OperationId,
                Produces = operation.Produces,
                Parameters = GetParameterList(operation.Parameters),
                Responses = GetResponseList(operation.Responses)
            };
        }

        private static ResponseInfo[] GetResponseList(Dictionary<int, ResponseModel> operationResponses)
        {
            return 
                (from item in operationResponses
                select new ResponseInfo
                {
                    StatusCode = item.Key
                })
                .ToArray();
        }

        private static ParameterInfo[] GetParameterList(ParameterModel[] operationParameters)
        {
            return
            (from item in operationParameters
             select new ParameterInfo
             {
                 In = item.In,
                 Required = item.Required,
                 Name = item.Name,
                 Type = item.Type
             })
             .ToArray();
        }

        private SwaggerModel DeserializeModel(string swaggerJson)
        {
            try
            {
                return _jsonSerializer.Deserialize<SwaggerModel>(swaggerJson);
            }
            catch (Exception ex)
            {
                throw new CoverageProcessingException("Failed to parse swagger document", ex);
            }
        }
    }
}

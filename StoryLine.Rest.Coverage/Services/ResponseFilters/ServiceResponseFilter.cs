using System;
using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.ResponseFilters
{
    public class ServiceResponseFilter : IResponseFilter
    {
        private readonly string _serviceName;

        public ServiceResponseFilter(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceName));

            _serviceName = serviceName;
        }

        public bool IsValid(Response response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (string.IsNullOrWhiteSpace(response.Request?.Service))
                return false;

            return response.Request.Service.Equals(_serviceName, StringComparison.OrdinalIgnoreCase);
        }
    }
}

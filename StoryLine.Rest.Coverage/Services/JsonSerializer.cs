using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StoryLine.Rest.Coverage.Services
{
    public class JsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public T Deserialize<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

            return JsonConvert.DeserializeObject<T>(content);
        }

        public string Serialize(object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return JsonConvert.SerializeObject(data, Formatting.Indented, Settings);
        }
    }
}
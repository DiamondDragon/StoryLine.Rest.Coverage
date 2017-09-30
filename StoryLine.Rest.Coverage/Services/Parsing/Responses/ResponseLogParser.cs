using System;
using StoryLine.Rest.Coverage.Model.Response;

namespace StoryLine.Rest.Coverage.Services.Parsing.Responses
{
    public class ResponseLogParser : IResponseLogParser
    {
        private readonly IJsonSerializer _serializer;

        public ResponseLogParser(IJsonSerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public ResponseLog Parse(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

            return new ResponseLog
            {
                Responses = _serializer.Deserialize<Response[]>(content)
            };
        }
    }
}
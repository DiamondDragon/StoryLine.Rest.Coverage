using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace StoryLine.Rest.Coverage.Model.Response
{
    public class Request
    {
        private string _url;

        public string Service { get; set; }
        public string Method { get; set; }

        public Dictionary<string, string[]> Headers = new Dictionary<string, string[]>();
        public Dictionary<string, string> Properties = new Dictionary<string, string>();
        public byte[] Body { get; set; }

        public string Url
        {
            get => _url;
            set
            {
                _url = value;

                var queryStringStartIndex = value.IndexOf("?", StringComparison.InvariantCultureIgnoreCase);

                UrlPath = queryStringStartIndex == -1 ? value : value.Substring(0, queryStringStartIndex);

                if (queryStringStartIndex > -1)
                    Query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(value.Substring(queryStringStartIndex + 1));
            }
        }

        [JsonIgnore]
        public string UrlPath { get; private set; }

        [JsonIgnore]
        public IReadOnlyDictionary<string, StringValues> Query { get; private set; } = new Dictionary<string, StringValues>(StringComparer.InvariantCultureIgnoreCase);
    }
}
using System.Collections.Generic;

namespace StoryLine.Rest.Coverage.Model.Response
{
    public class Response
    {
        public Request Request { get; set;  }
        public Dictionary<string, string[]> Headers = new Dictionary<string, string[]>();
        public Dictionary<string, string> Properties = new Dictionary<string, string>();
        public byte[] Body { get; set; }
        public int Status { get; set; }
        public string ReasonPhrase { get; set; }
    }
}
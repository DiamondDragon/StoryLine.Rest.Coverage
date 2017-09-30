using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoryLine.Rest.Coverage.Services.Content
{
    public class WebContentProvider : ISwaggerProvider, IResponseLogProvider
    {
        private readonly string _url;

        public WebContentProvider(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));

            _url = url;
        }

        public async Task<string> GetContent()
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(_url);
            }
        }
    }
}

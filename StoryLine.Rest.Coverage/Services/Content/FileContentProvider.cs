using System;
using System.IO;
using System.Threading.Tasks;

namespace StoryLine.Rest.Coverage.Services.Content
{
    public class FileContentProvider : ISwaggerProvider, IResponseLogProvider
    {
        private readonly string _filePath;

        public FileContentProvider(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));

            _filePath = filePath;
        }

        public async Task<string> GetContent()
        {
            return await File.ReadAllTextAsync(_filePath);
        }
    }
}

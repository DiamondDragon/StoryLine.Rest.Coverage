using System;
using System.IO;
using System.Threading.Tasks;
using StoryLine.Rest.Coverage.Services.Analyzers;

namespace StoryLine.Rest.Coverage.Services
{
    public class ReportPersister : IReportPersister
    {
        private readonly string _outputFile;
        private readonly IJsonSerializer _serializer;

        public ReportPersister(string outputFile, IJsonSerializer serializer)
        {
            if (string.IsNullOrWhiteSpace(outputFile))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(outputFile));

            _outputFile = outputFile;
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Save(IAnalysisReport[] reports)
        {
            if (reports == null)
                throw new ArgumentNullException(nameof(reports));

            await File.WriteAllTextAsync(_outputFile, _serializer.Serialize(reports));
        }
    }
}

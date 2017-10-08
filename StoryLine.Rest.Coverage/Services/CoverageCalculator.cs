using System;
using System.Linq;
using System.Threading.Tasks;
using StoryLine.Rest.Coverage.Exceptions;
using StoryLine.Rest.Coverage.Services.Content;
using StoryLine.Rest.Coverage.Services.Factories;
using StoryLine.Rest.Coverage.Services.Parsing.Responses;
using StoryLine.Rest.Coverage.Services.Parsing.Swagger;

namespace StoryLine.Rest.Coverage.Services
{
    public class CoverageCalculator : ICoverageCalculator
    {
        private readonly IReportPersister _reportPersister;
        private readonly IApiAnalyzerFactory _analyzerFactory;
        private readonly IResponseLogParser _responseLogParser;
        private readonly IResponseLogProvider _responseLogProvider;
        private readonly ISwaggerParser _swaggerParser;
        private readonly ISwaggerProvider _swaggerProvider;

        public CoverageCalculator(
            ISwaggerProvider swaggerProvider, 
            ISwaggerParser swaggerParser,
            IResponseLogProvider responseLogProvider,
            IResponseLogParser responseLogParser,
            IApiAnalyzerFactory analyzerFactory,
            IReportPersister reportPersister)
        {
            _swaggerProvider = swaggerProvider ?? throw new ArgumentNullException(nameof(swaggerProvider));
            _swaggerParser = swaggerParser ?? throw new ArgumentNullException(nameof(swaggerParser));
            _responseLogProvider = responseLogProvider ?? throw new ArgumentNullException(nameof(responseLogProvider));
            _responseLogParser = responseLogParser ?? throw new ArgumentNullException(nameof(responseLogParser));
            _analyzerFactory = analyzerFactory ?? throw new ArgumentNullException(nameof(analyzerFactory));
            _reportPersister = reportPersister ?? throw new ArgumentNullException(nameof(reportPersister));
        }

        public async Task Calculate()
        {
            var content =  await _swaggerProvider.GetContent();
            var responseLogContent = await _responseLogProvider.GetContent();

            if (string.IsNullOrWhiteSpace(content))
                throw new CoverageProcessingException("Failed to resolve swagger content.");

            if (string.IsNullOrWhiteSpace(responseLogContent))
                throw new CoverageProcessingException("Failed to resolve response log content.");

            var model = _swaggerParser.Parse(content);
            var responseLog = _responseLogParser.Parse(responseLogContent);

            var analyzer = _analyzerFactory.Create(model);

            foreach (var request in responseLog.Responses)
            {
                analyzer.Process(request);
            }

            await _reportPersister.Save(analyzer.GetReports().ToArray());
        }
    }
}

using System;

namespace StoryLine.Rest.Coverage.Exceptions
{
    public class CoverageProcessingException : Exception
    {
        public CoverageProcessingException()
        {
        }

        public CoverageProcessingException(string message)
            : base(message)
        {
        }

        public CoverageProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

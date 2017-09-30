using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StoryLine.Rest.Coverage.Services.Analyzers.Helpers
{
    public sealed class RegexInfo
    {
        public Regex Pattern { get; }
        public IReadOnlyDictionary<string, string> GroupToParamMapping { get; }

        public RegexInfo(Regex pattern, IReadOnlyDictionary<string, string> groupToParamMapping)
        {
            Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
            GroupToParamMapping = groupToParamMapping ?? throw new ArgumentNullException(nameof(groupToParamMapping));
        }
    }
}
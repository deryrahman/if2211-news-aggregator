using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsAggregator.Search
{
    public class RegexSearcher : Searcher
    {
        public RegexSearcher(string pattern) : base(pattern)
        {

        }

        public override int CheckMatch(string text)
        {
            Match match = Regex.Match(text.ToLower(), Pattern);

            if (!(match.Success))
            {
                return -1;
            }
            else
            {
                return match.Index;
            }
        }

        public override void SetPattern(string pattern)
        {
            Pattern = pattern.ToLower();
        }
    }
}
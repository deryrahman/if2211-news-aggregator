using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace NewsAggregator.Models
{
    public class RegexSearcher : Searcher
    {
        public RegexSearcher(string pattern) : base(pattern)
        {

        }

        public override int CheckMatch(string text)
        {
            Match match = Regex.Match(text, Pattern);

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
            Pattern = pattern;
        }
    }
}
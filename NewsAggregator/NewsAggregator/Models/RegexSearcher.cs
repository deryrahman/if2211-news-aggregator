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

        public override bool CheckMatch(string text)
        {
            return Regex.IsMatch(text, Pattern);  
        }

        public override void SetPattern(string pattern)
        {
            Pattern = pattern;
        }
    }
}
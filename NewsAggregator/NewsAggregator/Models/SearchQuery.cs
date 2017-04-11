using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAggregator.Models
{
    public class SearchQuery
    {
        public int id { get; set; }
        public string pattern { get; set; }
    }
}
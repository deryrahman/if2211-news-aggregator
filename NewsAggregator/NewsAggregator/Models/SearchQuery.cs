﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAggregator.Models
{
    public class SearchQuery
    {
        public int Id { get; set; }
        public string Pattern { get; set; }
    }
}
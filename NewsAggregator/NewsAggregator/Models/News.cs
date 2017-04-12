using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAggregator.Models
{
    public class News
    {
        public string Url { get; }
        public string Title { get; }
        public string Content { get; }
        
        public News(string url, string title, string content)
        {
            Url = String.Copy(url);
            Title = String.Copy(title);
            Content = String.Copy(content);
        }
    }
}
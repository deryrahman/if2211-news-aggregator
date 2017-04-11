using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAggregator.Models
{
    public class News
    {
        public string url { get; }
        public string title { get; }
        public string content { get; }
        
        public News(string url, string title, string content)
        {
            this.url = String.Copy(url);
            this.title = String.Copy(title);
            this.content = String.Copy(content);

        }
    }
}
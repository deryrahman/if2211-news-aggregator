using Newtonsoft.Json;
using System.Collections.Generic;

namespace NewsAggregator.Models
{
    public class News
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string PubDate { get; set; }

        public static List<News> GetNewsList(string path)
        {
            try
            {
                string json = System.IO.File.ReadAllText(path);

                return JsonConvert.DeserializeObject<List<News>>(json);
            }
            catch
            {
                return new List<News>();
            }
        }
    }
}
using AngleSharp.Parser.Html;
using NewsAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NewsAggregator.Scraper
{
    public class DetikScraper
    {
        private static string RSS = "http://rss.detik.com/index.php/detikcom";
        public static string PostFix = "detik";
        
        private static void GetContent(News news)
        {
            try
            {
                HtmlParser parser = new HtmlParser();
                WebClient client = new WebClient();

                var document = parser.Parse(client.DownloadString(news.Url));
                var isi = document.All.Where(m => m.LocalName == "div" && (m.ClassList.Contains("detail_text") || m.ClassList.Contains("text_detail") || m.ClassList.Contains("read__content")));
                
                news.Content = isi.ToArray()[0].TextContent;
            }
            catch
            {
                news.Content = "";
            }
        }

        public static List<News> Scrape()
        {
            List<News> result = RSSScraper.GetTitleAndURL(RSS);

            foreach (News news in result)
            {
                GetContent(news);
            }

            return result;
        }
    }
}
using NewsAggregator.Models;
using NewsAggregator.Scraper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace NewsAggregator.Controllers
{
    public class ScraperController : ApiController
    {
        private void CleanContent(News news)
        {
            string result = news.Content.Replace("\n", "").Trim();
            news.Content = Regex.Replace(result, @"\s+", " ");
        }

        public ReturnObject Get()
        {
            try
            {
                List<KeyValuePair<string, IEnumerable<News>>> daftarBerita = new List<KeyValuePair<string, IEnumerable<News>>>();
                daftarBerita.Add(new KeyValuePair<string, IEnumerable<News>>(DetikScraper.PostFix, DetikScraper.Scrape()));

                foreach (var berita in daftarBerita)
                {
                    foreach (News news in berita.Value)
                    {
                        CleanContent(news);
                    }

                    string path = System.Web.Hosting.HostingEnvironment.MapPath("~/NewsStore/" + berita.Key + ".txt");

                    using (StreamWriter file = new StreamWriter(path))
                    {
                        file.WriteLine(JsonConvert.SerializeObject(berita.Value));
                    }
                }

                return new ReturnObject() { status = true, data = null };
            }
            catch (Exception e)
            {
                return new ReturnObject() { status = false, data = e.Message };
            }
        }
    }
}
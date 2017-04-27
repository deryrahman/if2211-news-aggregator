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

        private void Combine (List<News> destination, List<News> source)
        {
            foreach (News news in source)
            {
                bool unique = true;

                foreach (News newsDest in destination)
                {
                    if (news.Url == newsDest.Url)
                    {
                        unique = false;

                        if (newsDest.Content.Equals(""))
                        {
                            newsDest.Content = news.Content;
                        }
                    }
                }

                if (unique)
                {
                    destination.Add(news);
                }
            }
        }

        private string GetPath(string postFix)
        {       
            string prefix = System.Web.Hosting.HostingEnvironment.MapPath("~/NewsStore/");
            return prefix + postFix + ".json";
        }
        private KeyValuePair<string, KeyValuePair<List<News>, List<News>>> DaftarBeritaEntry (string postFix, List<News> Scraped)
        {
            return new KeyValuePair<string, KeyValuePair<List<News>, List<News>>>(postFix, new KeyValuePair<List<News>, List<News>>(News.GetNewsList(GetPath(postFix)), Scraped));
        }

        public ReturnObject Get()
        {
            try
            {
                List<KeyValuePair<string, KeyValuePair<List<News>, List<News>>>> daftarBerita = new List<KeyValuePair<string, KeyValuePair<List<News>, List<News>>>>();

                try
                {
                    daftarBerita.Add(DaftarBeritaEntry(DetikScraper.PostFix, DetikScraper.Scrape()));
                }
                catch (Exception e)
                {

                }

                try
                {
                    daftarBerita.Add(DaftarBeritaEntry(TempoScraper.PostFix, TempoScraper.Scrape()));
                }
                catch (Exception e)
                {

                }

                try
                {
                    daftarBerita.Add(DaftarBeritaEntry(VivaScraper.PostFix, VivaScraper.Scrape()));
                }
                catch (Exception e)
                {

                }

                foreach (var berita in daftarBerita)
                {
                    foreach (News news in berita.Value.Key)
                    {
                        CleanContent(news);
                    }

                    foreach (News news in berita.Value.Value)
                    {
                        CleanContent(news);
                    }

                    Combine(berita.Value.Key, berita.Value.Value);

                    using (StreamWriter file = new StreamWriter(GetPath(berita.Key)))
                    {
                        file.WriteLine(JsonConvert.SerializeObject(berita.Value.Key));
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
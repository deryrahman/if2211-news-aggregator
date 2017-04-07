using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        private static bool isMatch(string text, string expr)
        {
            MatchCollection mc = Regex.Matches(text, expr);
            return mc.Count > 0;
        }
        static void Main(string[] args)
        {
            string[] berita = { "Detik", "Tempo", "Viva", "AntaraNews" };
            var berita_link = new Dictionary<string, string>();
            berita_link.Add("Detik", "http://rss.detik.com/index.php/detikcom");
            berita_link.Add("Tempo", "http://tempo.co/rss/terkini");
            berita_link.Add("Viva", "http://rss.vivanews.com/get/all ");
            berita_link.Add("Antara", "http://www.antaranews.com/rss/terkini");

            Console.WriteLine("Daftar berita : ");
            var i = 1;
            foreach(var item in berita_link)
            {
                Console.WriteLine(i++ + ". " + item.Key);
            }
            var rssurl=Console.ReadLine();
            rssurl = berita_link[rssurl];
            Console.Write("Ketik keyword : ");
            var keyword = Console.ReadLine();
            List<News> result;
            result = SearchByKey(keyword, rssurl);
            
            foreach(News item in result)
            {
                Console.WriteLine("Judul : " + item.GetTitle());
                Console.WriteLine("Link : " + item.GetLink());
            }
            Console.WriteLine("Press enter to exit");
            Console.ReadKey();
        }

        public class News
        {
            private string title;
            private string link;

            public News(string title, string link)
            {
                this.title = title;
                this.link = link;
            }

            public string GetTitle()
            {
                return title;
            }

            public string GetLink()
            {
                return link;
            }
        }

        private static List<News> SearchByKey(string keyword, string rssurl)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rssurl);
            XmlNodeList nodeList = doc.GetElementsByTagName("channel");
            XmlNode channelNode = nodeList[0];
            var result = new List<News>();
            keyword = keyword.ToLower();
            foreach (XmlNode item in channelNode.ChildNodes)
            {
                if (item.Name == "item")
                {
                    string pattern = @"^(.*?("+keyword+")[^$]*)$";
                    bool mc = isMatch(item.InnerText.ToLower(), pattern);
                    if (mc)
                    {
                        string title = item.SelectSingleNode("title").InnerText.ToUpper();
                        string link = item.SelectSingleNode("link").InnerText;
                        result.Add(new News(title, link));
                    }
                }
            }


            return result;
        }
    }
}

using NewsAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace NewsAggregator.Scraper
{
    public class RSSScraper
    {
        private static int maxContent = 20;

        private static string GetImageUrl(string postFix, XmlNode item)
        {
            if ((postFix == "detik") || (postFix == "viva"))
            {
                return item.SelectSingleNode("enclosure").Attributes["url"].Value;
            }
            else if (postFix == "tempo")
            {
                return item.SelectSingleNode("image").InnerText;
            }
            else
            {
                return "";
            }
        }

        public static List<News> GetTitleAndURL(string rssUrl, string postFix)
        {
            List<News> result = new List<News>();

            XmlDocument rssXml = new XmlDocument();
            rssXml.Load(rssUrl);

            XmlNodeList nodeList = rssXml.GetElementsByTagName("channel");
            XmlNode channelNode = nodeList[0];

            int counter = 0;
            foreach(XmlNode item in channelNode.ChildNodes){
                if (counter >= maxContent) break;

                if (item.Name.Equals("item"))
                {
                    result.Add(new News() { Url = item.SelectSingleNode("link").InnerText, Title = item.SelectSingleNode("title").InnerText, ImageUrl = GetImageUrl(postFix, item), PubDate = item.SelectSingleNode("pubDate").InnerText});
                    counter++;
                }
            }
            
            return result;
        } 
    }
}
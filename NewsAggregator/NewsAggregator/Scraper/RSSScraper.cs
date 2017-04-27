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
        public static List<News> GetTitleAndURL(string rssUrl)
        {
            List<News> result = new List<News>();

            XmlDocument rssXml = new XmlDocument();
            rssXml.Load(rssUrl);

            XmlNodeList nodeList = rssXml.GetElementsByTagName("channel");
            XmlNode channelNode = nodeList[0];

            foreach(XmlNode item in channelNode.ChildNodes){
                if (item.Name.Equals("item"))
                {
                    result.Add(new News() { Url = item.SelectSingleNode("link").InnerText, Title = item.SelectSingleNode("title").InnerText, ImageUrl = item.SelectSingleNode("enclosure").Attributes["url"].Value });
                }
            }

            return result;
        } 
    }
}
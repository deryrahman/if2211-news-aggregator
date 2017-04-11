using NewsAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewsAggregator.Controllers
{
    public class SearchController : ApiController
    {
        List<News> newsList = new List<News>();

        private void prepareNewsList()
        {
            newsList.Add(new News("http://google.com", "Ini google", "Halo ini google"));
            newsList.Add(new News("https://turfa.cf", "Blog turfa", "Kalo yang ini blog Turfa"));
        }

        public IEnumerable<News> Post(SearchQuery query)
        {
            List<News> result = new List<News>();
            KmpSearcher kmp = new KmpSearcher(query.pattern.ToLower());

            prepareNewsList();
            foreach(News news in newsList)
            {
                if ((kmp.checkMatch(news.title.ToLower())) || (kmp.checkMatch(news.content.ToLower())))
                {
                    result.Add(news);
                }
            }
            
            return result;
        }
    }
}
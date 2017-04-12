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

        private void PrepareNewsList()
        {
            newsList.Add(new News("http://google.com", "Ini google", "Halo ini google"));
            newsList.Add(new News("https://turfa.cf", "Blog turfa", "Kalo yang ini blog Turfa"));
        }

        public ReturnObject Post(SearchQuery query)
        {
            try
            {
                List<News> result = new List<News>();
                Searcher searcher;

                if (query.Id == 0)
                {
                    searcher = new KmpSearcher(query.Pattern.ToLower());
                }
                else
                {
                    searcher = new RegexSearcher(query.Pattern.ToLower());
                }

                PrepareNewsList();
                foreach (News news in newsList)
                {
                    if ((searcher.CheckMatch(news.Title.ToLower())) || (searcher.CheckMatch(news.Content.ToLower())))
                    {
                        result.Add(news);
                    }
                }
                return (new ReturnObject() { status = true, data = result });
            }
            catch (Exception e)
            {
                List<Exception> returnError = new List<Exception>();
                returnError.Add(e);
                return (new ReturnObject() { status = false, data = e.Message });
            }
        }
    }
}
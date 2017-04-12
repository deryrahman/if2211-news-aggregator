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
                List<SearchResult> result = new List<SearchResult>();
                Searcher searcher;

                if (query.Id == 0)
                {
                    searcher = new KmpSearcher(query.Pattern);
                }
                else
                {
                    searcher = new RegexSearcher(query.Pattern);
                }

                PrepareNewsList();

                foreach (News news in newsList)
                {
                    SearchResult searchResult = new SearchResult() { Url = news.Url, Title = news.Title };
                    bool found = false;

                    int indexMatchContent = searcher.CheckMatch(news.Content);
                    if (indexMatchContent != -1)
                    {
                        found = true;
                        searchResult.Match = SearchResult.StringToMatch(news.Content, query.Pattern, indexMatchContent, 10);
                    }
                    else
                    {
                        int indexMatchTitle = searcher.CheckMatch(news.Title);
                        if (indexMatchTitle != -1)
                        {
                            found = true;
                            searchResult.Match = SearchResult.StringToMatch(news.Title, query.Pattern, indexMatchTitle, 5) + " (Pada Judul)";
                        }
                    }

                    if (found)
                    {
                        result.Add(searchResult);
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
using NewsAggregator.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using NewsAggregator.Scraper;
using NewsAggregator.Search;

namespace NewsAggregator.Controllers
{
    public class SearchController : ApiController
    {
        List<News> newsList = new List<News>();

        private void PrepareNewsList()
        {
            string[] scrapers = new string[] { DetikScraper.PostFix };
            string prefix = System.Web.Hosting.HostingEnvironment.MapPath("~/NewsStore/");

            foreach (string scraper in scrapers)
            {
                string json = System.IO.File.ReadAllText(prefix + scraper + ".txt");
                newsList.AddRange(JsonConvert.DeserializeObject<List<News>>(json));
            }
        }

        public ReturnObject Post(SearchQuery query)
        {
            try
            {
                List<SearchResult> result = new List<SearchResult>();
                Searcher searcher;

                if (query.Pattern == null)
                {
                    query.Pattern = "";
                }

                query.Pattern = query.Pattern.Trim();

                if (query.Pattern.Equals(""))
                {
                    throw new ArgumentException("Pattern kosong.");
                }
                
                if (query.Id == 0)
                {
                    searcher = new KmpSearcher(query.Pattern);
                }
                else if (query.Id == 1)
                {
                    searcher = new BoyerMooreSearcher(query.Pattern);
                }
                else if (query.Id == 2)
                {
                    searcher = new RegexSearcher(query.Pattern);
                }
                else
                {
                    throw new NotImplementedException();
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
                        searchResult.Match = SearchResult.StringToMatch(news.Content, query.Pattern, indexMatchContent, 50);
                    }
                    else
                    {
                        int indexMatchTitle = searcher.CheckMatch(news.Title);
                        if (indexMatchTitle != -1)
                        {
                            found = true;
                            searchResult.Match = SearchResult.StringToMatch(news.Title, query.Pattern, indexMatchTitle, 10) + " (Pada Judul)";
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
                return (new ReturnObject() { status = false, data = e.Message });
            }
        }
    }
}
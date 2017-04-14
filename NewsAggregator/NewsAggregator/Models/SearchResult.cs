using System;

namespace NewsAggregator.Models
{
    public class SearchResult
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Match { get; set; }

        public static string StringToMatch(string text, string pattern, int foundIndex, int tailLength)
        {
            int startIndexCopy = Math.Max(foundIndex - tailLength, 0);
            int lengthCopy = Math.Min(pattern.Length + tailLength*2, text.Length - startIndexCopy);

            string tmp = text.Substring(startIndexCopy, lengthCopy);

            if (startIndexCopy != 0)
            {
                tmp = "..." + tmp;
            }

            if (startIndexCopy + lengthCopy < text.Length)
            {
                tmp = tmp + "...";
            }

            return tmp;
        }
    }
}
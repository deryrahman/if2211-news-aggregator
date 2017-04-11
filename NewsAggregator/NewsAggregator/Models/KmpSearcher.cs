using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAggregator.Models
{
    class KmpSearcher
    {
        private string pattern;
        private int[] suffpost;

        private void preprocess()
        {
            int len = 0;
            int i = 1;

            suffpost = new int[pattern.Length];

            suffpost[0] = 0;
            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    suffpost[i] = len;
                    i++;
                }
                else if (len != 0)
                {
                    len = suffpost[len - 1];
                }
                else
                {
                    suffpost[i] = 0;
                    i++;
                }
            }
        }

        public KmpSearcher(string _pattern)
        {
            setPattern(_pattern);
        }

        public void setPattern(string _pattern)
        {
            pattern = String.Copy(_pattern);
            preprocess();
        }

        public bool checkMatch(string text)
        {
            int i = 0;
            int j = 0;

            while (i < text.Length)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }

                if (j == pattern.Length)
                {
                    return true;
                }
                else if ((i < text.Length) && (pattern[j] != text[i]))
                {
                    if (j != 0)
                    {
                        j = suffpost[j - 1];
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
            }

            return false;
        }

        public static bool checkMatch(string text, string pattern)
        {
            KmpSearcher kmp = new KmpSearcher(pattern);
            return kmp.checkMatch(text);
        }
    }
}
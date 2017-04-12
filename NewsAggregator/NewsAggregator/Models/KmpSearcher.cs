using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAggregator.Models
{
    class KmpSearcher : Searcher
    {
        private int[] suffpost;
        
        public KmpSearcher(string pattern) : base(pattern)
        {
            
        }

        private void Preprocess()
        {
            int len = 0;
            int i = 1;

            suffpost = new int[Pattern.Length];

            suffpost[0] = 0;
            while (i < Pattern.Length)
            {
                if (Pattern[i] == Pattern[len])
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

        public override void SetPattern(string pattern)
        {
            Pattern = String.Copy(pattern);
            Preprocess();
        }

        public override bool CheckMatch(string text)
        {
            int i = 0;
            int j = 0;

            while (i < text.Length)
            {
                if (Pattern[j] == text[i])
                {
                    j++;
                    i++;
                }

                if (j == Pattern.Length)
                {
                    return true;
                }
                else if ((i < text.Length) && (Pattern[j] != text[i]))
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

        public static bool CheckMatch(string text, string pattern)
        {
            KmpSearcher kmp = new KmpSearcher(pattern);
            return kmp.CheckMatch(text);
        }
    }
}
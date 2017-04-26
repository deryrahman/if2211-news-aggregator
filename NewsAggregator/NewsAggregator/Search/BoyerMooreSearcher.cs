using System;

namespace NewsAggregator.Search
{
    public class BoyerMooreSearcher : Searcher
    {
        private int[] lastOccurence;

        /**
        * Compute array LastOccurence storing index of last occurence of each ASCII
        * char in pattern
        */
        private void Preprocess()
        {
            //make an array of char set
            lastOccurence = new int[Char.MaxValue];
            
            //array initialization
            for (int i = 0; i < Char.MaxValue; i++)
            {
                lastOccurence[i] = -1;
            }

            //storing index of last occurence of each char in pattern
            for (int i = 0; i < Pattern.Length; i++)
            {
                lastOccurence[(int) Pattern[i]] = i;
            }
        }

        public BoyerMooreSearcher(string pattern) : base(pattern)
        {
        }

        public override int CheckMatch(string text)
        {
            text = text.ToLower();

            int retVal = -1;

            int i = Pattern.Length - 1;//initial pointer in text is based on last index of pattern
            //case when pattern is longer than text
            if (i > text.Length)
            {
                return retVal;
            }
            else
            {
                int j = Pattern.Length - 1; //pointer in pattern always starts from last index of pattern
                do
                {
                    if (Pattern[j] == text[i])
                    {
                        //match until first index of pattern
                        if (j == 0)
                        {
                            retVal = i;
                            return retVal;
                        }
                        //match before first index of pattern
                        else
                        {
                            i--;
                            j--;
                        }
                    }
                    //when dismacth do the character jump technique
                    else
                    {
                        int charLastOcc = lastOccurence[text[i]];
                        i = i + Pattern.Length - Math.Min(j, 1 + charLastOcc); //pointer in text jumps
                        j = Pattern.Length - 1;  //pointer in pattern always starts from last index of pattern
                    }
                } while (i < text.Length);
                return retVal; //-1 when no match found
            }
        }

        public override void SetPattern(string pattern)
        {
            Pattern = pattern.ToLower();
            Preprocess();
        }
    }
}
//author Rizky Faramita
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoyerMoore
{
    class Program
    {
 
        /**
         * Boyer Moore Algorithm for Pattern Matching
         */
        public static int BoyerMoore(string text, string pattern)
        {
            int retVal = -1;
            int[] lastOccurence = getLastOccurence(pattern);

            int i = pattern.Length - 1;//initial pointer in text is based on last index of pattern
            //case when pattern is longer than text
            if (i > text.Length) 
            {
                Console.WriteLine("No match pattern!");
                return retVal;
            }
            else
            {
                int j = pattern.Length -1 ; //pointer in pattern always starts from last index of pattern
                do
                {
                    if (pattern[j] == text[i])
                    {
                        //match until first index of pattern
                        if (j == 0)
                        {
                            retVal = i;
                            Console.WriteLine("Match pattern found!");
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
                        i = i + pattern.Length - Math.Min(j, 1 + charLastOcc); //pointer in text jumps
                        j = pattern.Length - 1;  //pointer in pattern always starts from last index of pattern
                    }
                } while (i < text.Length);
                return retVal; //-1 when no match found
            }
        }
 
        /**
         * return array storing index of last occurence of each ASCII
         * char in pattern
         */
        private static int[] getLastOccurence(string pattern)
        {
            //make an array of ASCII char set
            int[] lastOccurence = new int[128];
            //array initialization
            for (int i = 0; i < 128; i++)
            {
                lastOccurence[i] = -1;
            }
            //storing index of last occurence of each char in pattern
            for (int i = 0; i < pattern.Length; i++)
            {
                lastOccurence[(int)pattern[i]] = i;
            }
            return lastOccurence;
        }

        static void Main(string[] args)
        {
            string sampleText = "abdedeabd";
            int retVal = BoyerMoore(sampleText, "abc");
            Console.WriteLine("Pattern matches at index: " + retVal);
            string test = Console.ReadLine();
        }
    }
}
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace _4._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            string punctuation = "\\s,.;:!?()\\-";
            Console.WriteLine("Palindromų kiekis: {0, 3:d}", TaskUtils.Process(CFd, punctuation));
        }
    }

    class TaskUtils
    {
        /** Reads file and counts the number of palindrome words.
        @param fin – name of data file
        @param punctuation – punctuation marks to separate words */
        public static int Process(string fin, string punctuation)
        {
            string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
            int palindromeCount = 0;
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    palindromeCount += CountPalindromes(line, punctuation);
                }
            }
            return palindromeCount;
        }

        /** Splits line into words and counts palindromes.
        @param line – string of data
        @param punctuation – punctuation marks to separate words */
        private static int CountPalindromes(string line, string punctuation)
        {
            string[] parts = Regex.Split(line, "[" + punctuation + "]+");
            int palindromeCount = 0;
            foreach (string word in parts)
            {
                if (word.Length > 0 && IsPalindrome(word))
                {
                    palindromeCount++;
                }
            }
            return palindromeCount;
        }

        /** Checks if a word is a palindrome.
        @param word – word to check */
        private static bool IsPalindrome(string word)
        {
            word = word.ToLower(); // Make the comparison case-insensitive
            int left = 0;
            int right = word.Length - 1;
            while (left < right)
            {
                if (word[left] != word[right])
                {
                    return false;
                }
                left++;
                right--;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace _4._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            LettersFrequency letters = new LettersFrequency();

            // Count letter frequencies
            InOut.Repetitions(CFd, letters);

            // Print letter frequencies to file in descending order
            InOut.PrintRepetitions(CFr, letters);
        }
    }

    /** To count letters frequency */
    class LettersFrequency
    {
        // Expanded max character set to include Lithuanian characters
        private const int CMax = 512;  // Expanded size to support Lithuanian characters
        private int[] Frequency; // Frequency of letters
        public string line { get; set; }

        public LettersFrequency()
        {
            line = "";
            Frequency = new int[CMax];
            for (int i = 0; i < CMax; i++)
                Frequency[i] = 0;
        }

        public int Get(char character)
        {
            return Frequency[character];
        }

        /** Counts repetition of letters, including Lithuanian characters. */
        public void Count()
        {
            // Lithuanian specific characters
            char[] lithuanianChars = new char[] {'ą', 'č', 'ę', 'ė', 'į', 'š', 'ų', 'ū', 'ž',
                                                  'Ą', 'Č', 'Ę', 'Ė', 'Į', 'Š', 'Ų', 'Ū', 'Ž'};

            for (int i = 0; i < line.Length; i++)
            {
                char currentChar = line[i];

                // Check for Lithuanian characters and alphabet (lowercase, uppercase, and Lithuanian-specific)
                if (('a' <= currentChar && currentChar <= 'z') ||
                    ('A' <= currentChar && currentChar <= 'Z') ||
                    Array.Exists(lithuanianChars, ch => ch == currentChar))  // Manually check against Lithuanian letters
                {
                    Frequency[currentChar]++;
                }
            }
        }

        public int[] GetFrequencies()
        {
            return Frequency;
        }
    }

    /** Utility class for data manipulation */
    static class DataUtils
    {
        /** Generates a list of letter-frequency pairs from the LettersFrequency object. */
        public static List<(char, int)> GetLetterFrequencyList(LettersFrequency letters)
        {
            var frequencies = letters.GetFrequencies();
            var list = new List<(char, int)>();

            for (int i = 0; i < frequencies.Length; i++)
            {
                if (frequencies[i] > 0)
                {
                    list.Add(((char)i, frequencies[i]));
                }
            }

            return list;
        }
    }

    /** Utility class for text analysis tasks */
    static class TaskUtils
    {
        /** Sorts a list of letter-frequency pairs in descending order by frequency. */
        public static void SortDescendingByFrequency(List<(char, int)> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j].Item2 < list[j + 1].Item2)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
    }

    static class InOut
    {
        /** Prints repetition of letters sorted by frequency into a given file. */
        public static void PrintRepetitions(string fout, LettersFrequency letters)
        {
            // Step 1: Generate unsorted list of letter frequencies
            var frequencyList = DataUtils.GetLetterFrequencyList(letters);

            // Step 2: Sort the list in descending order of frequency
            TaskUtils.SortDescendingByFrequency(frequencyList);

            // Step 3: Write the sorted data to the file in the original two-column format
            using (var writer = File.CreateText(fout))
            {
                // Print sorted letter frequencies
                foreach (var (letter, frequency) in frequencyList)
                {
                    if (char.IsLower(letter))
                    {
                        // Print lowercase and uppercase letter frequencies
                        writer.WriteLine("{0, 3:c} {1, 4:d} |{2, 3:c} {3, 4:d}",
                            letter, frequency,
                            char.ToUpper(letter), frequency);
                    }
                }
            }
        }

        /** Inputs from the given data file and counts repetition of letters. */
        public static void Repetitions(string fin, LettersFrequency letters)
        {
            using (StreamReader reader = new StreamReader(fin))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    letters.line = line;
                    letters.Count();
                }
            }
        }
    }
}

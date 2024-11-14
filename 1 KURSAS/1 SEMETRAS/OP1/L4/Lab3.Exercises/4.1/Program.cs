using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _4._1
{
    class Program
    {
        //------------------------------------------------------------
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            LettersFrequency letters = new LettersFrequency();
            InOut.Repetitions(CFd, letters);
            InOut.PrintRepetitions(CFr, letters);
            TaskUtils.FindHighestFreqLetter(letters);
        }
    }
    static class InOut
    {
        //------------------------------------------------------------
        /** Prints repetition of letters using two columns into a given file.
        @param fout – name of the file for the output
        @param letters – object having letters and their repetitions */
        public static void PrintRepetitions(string fout, LettersFrequency letters)
        {
            using (var writer = File.CreateText(fout))
            {
                for (char ch = 'a'; ch <= 'z'; ch++)
                    writer.WriteLine("{0, 3:c} {1, 4:d} |{2, 3:c} {3, 4:d}", ch,
                   letters.Get(ch), Char.ToUpper(ch), letters.Get(Char.ToUpper(ch)));
            }
        }

        //------------------------------------------------------------
        /** Inputs from the given data file and counts repetition of letters.
         @param fin – name of data file
         @param letters – object having letters and their repetitions*/
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
        //------------------------------------------------------------
    }
    //------------------------------------------------------------

    //------------------------------------------------------------
    /** To count letters frequency */
    class LettersFrequency
    {
        private const int CMax = 256;
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
        //------------------------------------------------------------
        /** Counts repetition of letters. */
        public void Count()
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (('a' <= line[i] && line[i] <= 'z') ||
                ('A' <= line[i] && line[i] <= 'Z'))
                    Frequency[line[i]]++;
            }
        }
    }
    //------------------------------------------------------------

    class TaskUtils
    {
        public int FindHighestFreqLetter(LettersFrequency frequency)
        {
            int maxFreq = 0;
            int index = 0;

            for (char ch = 'a'; ch <= 'z'; index++)
            {
                if(frequency.Get(ch) >= maxFreq)
                {
                    maxFreq = frequency.Get(ch);
                }
            }

            for(char ch = 'A'; ch <= 'Z';  index++)
            {
                if(frequency.Get(ch) >= maxFreq)
                {
                    maxFreq = frequency.Get(ch);
                }
            }

            return maxFreq;
        }
    }
}

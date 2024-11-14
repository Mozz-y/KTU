using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            int No = InOut.LongestLine(CFd);
            InOut.RemoveLine(CFd, CFr, No);
            Console.WriteLine("Ilgiausios eilutės nr. {0, 4:d}", No + 1);
        }
    }

    class InOut
    {
        //------------------------------------------------------------
        /** Finds the ordinal number of the longest line.
         @param fin – name of data file
         returns the ordinal number of the longest line*/
        public static int LongestLine(string fin)
        {
            int No = -1;
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                int length = 0;
                int lineNo = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > length)
                    {
                        length = line.Length;
                        No = lineNo;
                    }
                    lineNo++;
                }
            }
            return No;
        }
        //------------------------------------------------------------
        //----------------------------------------------------------------
        /** Removes the line of the given ordinal number.
        @param fin – name of data file
        @param fout – name of result file
        @param No – the ordinal number of the longest line */
        public static void RemoveLine(string fin, string fout, int No)
        {
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                int lineNo = 0;
                using (var writer = File.CreateText(fout))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (No != lineNo)
                        {
                            writer.WriteLine(line);
                        }
                        lineNo++;
                    }
                }
            }
        }
    } //------------------------------------------------------------
}

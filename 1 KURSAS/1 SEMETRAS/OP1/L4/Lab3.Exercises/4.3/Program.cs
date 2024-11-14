using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace _4._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            const string CFa = "Analize.txt";
            InOut.Process(CFd, CFr, CFa);
        }
    }

    class InOut
    {
        //------------------------------------------------------------
        /** Reads, removes comments and prints to two files.
        @param fin – name of data file
        @param fout – name of result file
        @param finfo – name of informative file */
        public static void Process(string fin, string fout, string finfo)
        {
            string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
            using (var writerF = File.CreateText(fout))
            {
                using (var writerI = File.CreateText(finfo))
                {
                    foreach (string line in lines)
                    {
                        if (line.Length > 0)
                        {
                            string newLine = line;
                            if (RemoveComments(line, out newLine))
                                writerI.WriteLine(line);
                            if (newLine.Length > 0)
                                writerF.WriteLine(newLine);
                        }
                        else
                            writerF.WriteLine(line);
                    }
                }
            }
        }
        //------------------------------------------------------------
    }

    internal class TaskUtils
    {
        //------------------------------------------------------------
        /** Removes comments and returns an indication about performed activity.
        @param line – line having possible comments
        @param newLine – line without comments */
        public static bool RemoveComments(string line, out string newLine)
        {
            newLine = line;
            for (int i = 0; i < line.Length - 1; i++)
                if (line[i] == '/' && line[i + 1] == '/')
                {
                    newLine = line.Remove(i);
                    return true;
                }
            return false;
        }
        //------------------------------------------------------------
    }
}

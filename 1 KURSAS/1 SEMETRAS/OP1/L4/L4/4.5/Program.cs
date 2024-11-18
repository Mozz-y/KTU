using System.IO;
using System.Text;

namespace _4._5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            string punctuation = " .,!?:;()\t'";
            string name = "Arvydas";
            string surname = "Sabonis";
            TaskUtils.Process(CFd, CFr, punctuation, name, surname);
        }
    }

    class TaskUtils
    {
        /** Reads file and adds given surname to the given name.
         @param fin – name of data file
         @param fout – name of result file
         @param punctuation – punctuation marks to separate words
         @param name – word to find
         @param surname – given word to add */
        public static void Process(string fin, string fout, string punctuation,
       string name, string surname)
        {
            string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
            using (var writer = File.CreateText(fout))
            {
                foreach (string line in lines)
                {
                    StringBuilder newLine = new StringBuilder();
                    RemoveWordAndPunctuation(line, punctuation, name, newLine);
                    writer.WriteLine(newLine);
                }
            }
        }


        /** Finds name in the line and constructs new line appending given surname.
        @param line – string of data
        @param punctuation – punctuation marks to separate words
        @param name – word to find
        @param surname – given word to add
        @param newLine – string of result */
        private static void RemoveWordAndPunctuation(string line, string punctuation, string name, StringBuilder newLine)
        {
            string addLine = " " + line + " "; // Add spaces around the line for easier boundary checks
            int init = 1; // Start after the initial space
            int ind = addLine.IndexOf(name); // Find the first occurrence of the name

            while (ind != -1)
            {
                // Check if the name is isolated (surrounded by punctuation or spaces)
                if (punctuation.IndexOf(addLine[ind - 1]) != -1 && punctuation.IndexOf(addLine[ind + name.Length]) != -1)
                {
                    // Append the part of the line up to the name
                    newLine.Append(addLine.Substring(init, ind - init));
                    init = ind + name.Length; // Skip the name

                    // Remove trailing punctuation, if any
                    while (init < addLine.Length && punctuation.IndexOf(addLine[init]) != -1)
                    {
                        init++; // Skip any punctuation following the word
                    }
                }
                // Look for the next occurrence of the name
                ind = addLine.IndexOf(name, init);
            }

            // Append the rest of the line after the last matched name and punctuation
            newLine.Append(addLine.Substring(init));
        }
    }
}

using System.IO;
using System.Text;

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
        /** Reads, removes comments and prints to two files.
         @param fin – name of data file
         @param fout – name of result file
         @param finfo – name of informative file */
        public static void Process(string fin, string fout, string finfo)
        {
            string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
            bool inBlockComment = false; // Track if inside a multi-line comment
            using (var writerF = File.CreateText(fout))
            using (var writerI = File.CreateText(finfo))
            {
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        string newLine;
                        if (TaskUtils.RemoveComments(line, ref inBlockComment, out newLine)) // Pass state
                            writerI.WriteLine(line); // Write original line with comments removed to info file
                        if (newLine.Length > 0)
                            writerF.WriteLine(newLine); // Write the cleaned line to the result file
                    }
                    else
                        writerF.WriteLine(line);
                }
            }
        }

    }

    class TaskUtils
    {
        /** Removes comments and returns an indication about performed activity.
        @param line – line having possible comments
        @param newLine – line without comments */
        public static bool RemoveComments(string line, ref bool inBlockComment, out string newLine)
        {
            newLine = line;
            bool commentRemoved = false;

            if (inBlockComment)
            {
                // Already inside a multi-line comment; search for the end
                int blockEndIndex = newLine.IndexOf("*/");
                if (blockEndIndex >= 0)
                {
                    // End of the block comment found
                    newLine = newLine.Substring(blockEndIndex + 2); // Remove up to */
                    inBlockComment = false; // Exit block comment state
                    commentRemoved = true;
                }
                else
                {
                    // Entire line is part of the block comment
                    newLine = string.Empty;
                    commentRemoved = true;
                }
            }

            // Handle new block comments if not currently inside one
            if (!inBlockComment)
            {
                int blockStartIndex = newLine.IndexOf("/*");
                int blockEndIndex = newLine.IndexOf("*/");

                while (blockStartIndex >= 0)
                {
                    if (blockEndIndex > blockStartIndex)
                    {
                        // The block comment ends on the same line
                        newLine = newLine.Remove(blockStartIndex, blockEndIndex - blockStartIndex + 2);
                        commentRemoved = true;
                        // Check for another block comment in the same line
                        blockStartIndex = newLine.IndexOf("/*");
                        blockEndIndex = newLine.IndexOf("*/");
                    }
                    else
                    {
                        // The block comment starts but doesn't end on this line
                        newLine = newLine.Substring(0, blockStartIndex); // Remove from the start of the block comment
                        inBlockComment = true; // Enter block comment state
                        commentRemoved = true;
                        break;
                    }
                }
            }

            // Handle single-line comments
            int singleLineCommentIndex = newLine.IndexOf("//");
            if (singleLineCommentIndex >= 0)
            {
                newLine = newLine.Remove(singleLineCommentIndex);
                commentRemoved = true;
            }

            return commentRemoved;
        }

    }
}

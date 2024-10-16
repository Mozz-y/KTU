using System;
using System.Collections.Generic;

namespace U2_17.Register
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set console encoding to support special characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Paths to input files
            string org1FilePath = "Organizacija1.csv";
            string org2FilePath = "Organizacija2.csv";

            // Output file paths
            string outputFilePath = "rezultatai.txt";
            string themesOutputFilePath = "Temos.csv";

            // Create an instance of TaskUtils and load data
            TaskUtils taskUtils = new TaskUtils();
            List<Info> data = InOutUtils.ReadData(org1FilePath, org2FilePath);
            taskUtils.LoadData(data);

            // Output initial data
            InOutUtils.PrintInitialData(data, outputFilePath);

            // Output the most active authors
            Dictionary<string, List<(string Author, int Count)>> mostActiveAuthors = taskUtils.FindMostActiveAuthors();
            InOutUtils.PrintMostActiveAuthors(mostActiveAuthors);

            // Output the most difficult questions
            List<Info> mostDifficultQuestions = taskUtils.FindMostDifficultQuestions();
            InOutUtils.PrintMostDifficultQuestions(mostDifficultQuestions);

            // Output all themes
            List<string> allThemes = taskUtils.CollectAllThemes();
            InOutUtils.PrintAllThemes(allThemes, themesOutputFilePath);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace U2_17.Register
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set console encoding to support special characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Paths to the input CSV files for both organizations
            string org1FilePath = "Organizacija1.csv";
            string org2FilePath = "Organizacija2.csv";

            // Output file path
            string outputFilePath = "rezultatai.txt";
            string themesOutputFilePath = "Temos.csv";

            // Read data from both CSV files into a list of Info objects
            List<Info> data = InOutUtils.ReadData(org1FilePath, org2FilePath);

            // Write the read data to an output file
            InOutUtils.PrintInitialData(data, outputFilePath);

            // Create an instance of TaskUtils with the data
            TaskUtils taskUtils = new TaskUtils(data);

            // Find the top authors in each organization
            var topAuthorsByOrg = taskUtils.FindMostActiveAuthors();

            // Print the top authors to the console
            InOutUtils.PrintMostActiveAuthors(topAuthorsByOrg);

            // Find the most difficult questions
            var mostDifficultQuestions = TaskUtils.FindMostDifficultQuestions(data);

            // Print the most difficult questions
            InOutUtils.PrintMostDifficultQuestions(mostDifficultQuestions);

            // Collect all themes across both organizations
            List<string> themes = taskUtils.CollectAllThemes();

            // Print all themes to the specified CSV file
            InOutUtils.PrintAllThemes(themes, themesOutputFilePath);
        }
    }
}

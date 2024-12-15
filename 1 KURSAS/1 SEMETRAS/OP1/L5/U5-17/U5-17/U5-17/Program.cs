using System;
using System.Collections.Generic;
using System.Text;

namespace U5_17
{
    /// <summary>
    /// Entry point for the application, responsible for managing data reading, processing, and output.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method that orchestrates the reading of data files, performing operations on the data, and outputting results.
        /// </summary>
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // File paths for input data from three organizations
            string file1 = "Org1.csv"; // Organization 1 data file
            string file2 = "Org2.csv"; // Organization 2 data file
            string file3 = "Org3.csv"; // Organization 3 data file

            // File path for consolidated output in table format
            string outputFilePath = "rezultatai.txt";

            // Read data for each organization, extracting organization name and list of questions
            var (org1Name, org1Questions) = InOutUtils.ReadData(file1); // Read Org1 data
            var (org2Name, org2Questions) = InOutUtils.ReadData(file2); // Read Org2 data
            var (org3Name, org3Questions) = InOutUtils.ReadData(file3); // Read Org3 data

            // Print data for each organization into a consolidated file
            InOutUtils.PrintData(org1Name, org1Questions, outputFilePath); // Print Org1 data
            InOutUtils.PrintData(org2Name, org2Questions, outputFilePath); // Print Org2 data
            InOutUtils.PrintData(org3Name, org3Questions, outputFilePath); // Print Org3 data

            // Create a Register object, passing the lists of questions for all three organizations
            var register = new Register(org1Questions, org2Questions, org3Questions);

            // Compute the total counts of questions for each difficulty level across all organizations
            var totalDifficulties = register.ComputeTotalDifficulties(); // Get difficulty counts
            InOutUtils.PrintTotalDifficulties(totalDifficulties); // Output difficulty counts to console

            // Create a dictionary mapping organization names to their respective question lists
            var organizationData = new Dictionary<string, List<Question>>
            {
                { org1Name, org1Questions }, // Add Org1 data
                { org2Name, org2Questions }, // Add Org2 data
                { org3Name, org3Questions }  // Add Org3 data
            };

            // Find the top authors (and their question counts) for each organization
            var topAuthors = register.GetTopAuthors(organizationData); // Compute top authors
            InOutUtils.PrintTopAuthors(topAuthors); // Output top authors to console

            // Find all music questions with difficulty III across all organizations
            var allDifficultyIIIMusicQuestions = register.FindDifficultMusicQuestions(); // Filter music questions
            string outputMusicFile = "SudėtingiMuzikiniai.csv"; // Output file for music questions
            InOutUtils.PrintMostDifficultMusicQuestions(allDifficultyIIIMusicQuestions, outputMusicFile); // Output results

            // Find all questions with difficulty III across all organizations
            var allDifficultyIIIQuestions = register.FindAllDifficultyIIIQuestions(); // Filter difficulty III questions
            string outputDifficultyIIIFile = "SudėtingiBendri.csv"; // Output file for difficulty III questions
            InOutUtils.PrintMostDifficultQuestions(allDifficultyIIIQuestions, outputDifficultyIIIFile); // Output results

            // Find all "Linksmasis"-themed questions and sort by author and points
            var funQuestions = register.GetFunQuestions(); // Filter "Linksmasis" questions
            string outputFunQuestions = "Linksmieji.csv"; // Output file for "Linksmasis" questions
            InOutUtils.PrintFunQuestions(funQuestions, outputFunQuestions); // Output results
        }
    }
}

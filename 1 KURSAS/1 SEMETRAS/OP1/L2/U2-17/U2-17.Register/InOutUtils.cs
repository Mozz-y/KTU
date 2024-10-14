using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace U2_17.Register
{
    class InOutUtils
    {
        /// <summary>
        /// Reads data from both organization CSV files and returns a combined list of Info objects.
        /// </summary>
        /// <param name="file1">The path to the first organization's CSV file.</param>
        /// <param name="file2">The path to the second organization's CSV file.</param>
        /// <returns>A list of Info objects containing question data from both organizations.</returns>
        public static List<Info> ReadData(string file1, string file2)
        {
            List<Info> allData = new List<Info>();

            // Read data from the first organization file
            ReadDataFromFile(file1, allData);

            // Read data from the second organization file
            ReadDataFromFile(file2, allData);

            return allData;
        }

        /// <summary>
        /// Reads data from a specified CSV file and appends it to the provided list of Info objects.
        /// </summary>
        /// <param name="fileName">The path to the CSV file containing question data.</param>
        /// <param name="infoList">The list of Info objects to which the data will be added.</param>
        private static void ReadDataFromFile(string fileName, List<Info> infoList)
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            string organizationName = lines[0]; // The first line is the organization name

            // Loop through each line after the first to read question data
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(';'); // Split the line into values

                // Extract relevant information from the CSV format
                string theme = values[0];
                string difficulty = values[1];
                string author = values[2];
                string question = values[3];

                // Collect all possible answers in a list
                List<string> answers = new List<string>
                {
                    values[4], values[5], values[6], values[7]
                };

                string correctAnswer = values[8]; // Correct answer
                int points = int.Parse(values[9]); // Points for the question

                // Create an Info object and add it to the list
                Info info = new Info(organizationName, theme, difficulty, author, question, answers, correctAnswer, points);
                infoList.Add(info);
            }
        }

        /// <summary>
        /// Writes the question data to a specified output file in the desired format.
        /// </summary>
        /// <param name="infoList">The list of Info objects containing question data.</param>
        /// <param name="outputFile">The path to the output file.</param>
        public static void PrintInitialData(List<Info> infoList, string outputFile)
        {
            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                string currentOrganization = string.Empty;

                foreach (var info in infoList)
                {
                    // If the organization changes, write the organization name
                    if (info.Organization != currentOrganization)
                    {
                        if (!string.IsNullOrEmpty(currentOrganization)) // Add a blank line before the new organization, if not the first one
                        {
                            writer.WriteLine();
                        }
                        currentOrganization = info.Organization;
                        //writer.WriteLine(currentOrganization); // Write organization name
                    }

                    // Write question information using ToString()
                    writer.WriteLine(info.ToString());
                    writer.WriteLine(); // Blank line after each question for better readability
                }
            }
        }

        /// <summary>
        /// Prints the top authors for each organization to the console.
        /// </summary>
        /// <param name="topAuthorsByOrg">A dictionary containing top authors for each organization.</param>
        public static void PrintMostActiveAuthors(Dictionary<string, List<(string Author, int Count)>> topAuthorsByOrg)
        {
            foreach (var orgEntry in topAuthorsByOrg)
            {
                string organization = orgEntry.Key;
                var authors = orgEntry.Value;

                // Print organization name
                Console.WriteLine(organization);

                // Print the top authors and their question counts
                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.Author}: {author.Count}");
                }

                Console.WriteLine(); // Blank line between organizations
            }
        }

        /// <summary>
        /// Prints the most difficult questions to the console.
        /// </summary>
        /// <param name="mostDifficultQuestions">A list of Info objects containing the most difficult questions.</param>
        public static void PrintMostDifficultQuestions(List<Info> mostDifficultQuestions)
        {
            foreach (var question in mostDifficultQuestions)
            {
                Console.WriteLine(question.ToString());
            }
        }

        /// <summary>
        /// Writes all themes to "Temos.csv".
        /// </summary>
        /// <param name="themes">List of themes to write.</param>
        /// <param name="filePath">Path to the CSV file.</param>
        public static void PrintAllThemes(List<string> themes, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Write each theme to a new line in the file
                foreach (var theme in themes)
                {
                    writer.WriteLine(theme);
                }
            }
        }
    }
}

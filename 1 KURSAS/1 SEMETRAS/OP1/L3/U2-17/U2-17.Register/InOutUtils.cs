using System;
using System.Collections.Generic;
using System.Text;
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
        public static EventRegister ReadData(string file1, string file2)
        {
            EventRegister allData = new EventRegister();

            ReadDataFromFile(file1, allData);
            ReadDataFromFile(file2, allData);

            return allData;
        }

        /// <summary>
        /// Reads data from a specified CSV file and appends it to the provided list of Info objects.
        /// </summary>
        /// <param name="fileName">The path to the CSV file containing question data.</param>
        /// <param name="infoList">The list of Info objects to which the data will be added.</param>
        private static void ReadDataFromFile(string fileName, EventRegister allData)
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
                allData.Add(info);
            }
        }

        /// <summary>
        /// Writes the question data to a specified output file in the desired format.
        /// </summary>
        /// <param name="infoList">The list of Info objects containing question data.</param>
        /// <param name="outputFile">The path to the output file.</param>
        public static void PrintInitialData(EventRegister eventRegister, string outputFile)
        {
            using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                string currentOrganization = string.Empty;

                for (int i = 0; i < eventRegister.GetCount(); i++)
                {
                    var info = eventRegister.GetEntry(i);

                    if (info.Organization != currentOrganization)
                    {
                        if (!string.IsNullOrEmpty(currentOrganization))
                        {
                            writer.WriteLine();
                        }
                        currentOrganization = info.Organization;
                    }

                    writer.WriteLine(info.ToString());
                    writer.WriteLine();
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
        public static void PrintMostDifficultQuestions(EventContainer mostDifficultQuestions)
        {
            for(int i = 0; i < mostDifficultQuestions.Count; i++)
            {
                Console.WriteLine(mostDifficultQuestions.Get(i).ToString());
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

        public static void PrintDuplicates(EventContainer duplicates, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                if (duplicates == null || duplicates.Count == 0)
                {
                    writer.WriteLine("Vienodų klausimų nėra"); // Write message when no duplicates
                }
                else
                {
                    for(int i = 0; i < duplicates.Count; i++)
                    {
                        var info = duplicates.Get(i);
                        writer.WriteLine($"{info.Theme}, {info.Author}, {info.Question}"); // Write duplicate questions
                    }
                }
            }
        }

    }
}

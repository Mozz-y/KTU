using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab1.U17.Register
{
    static class InOutUtils
    {
        /// <summary>
        /// Read data from the specified CSV file and return a list of Info objects.
        /// </summary>
        /// <param name="fileName">The path to the CSV file containing question data.</param>
        /// <returns>A list of Info objects representing the questions from the CSV file.</returns>
        public static List<Info> ReadData(string fileName)
        {
            List<Info> data = new List<Info>();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8); // Read all lines from the file

            foreach (string line in lines)
            {
                string[] values = line.Split(';'); // Split the line into values

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
                Info info = new Info(theme, difficulty, author, question, answers, correctAnswer, points);
                data.Add(info);
            }

            return data; // Return the populated list of Info objects
        }

        /// <summary>
        /// Writes the initial data to a specified text file in table format.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        /// <param name="fileName">The name of the file to write the output.</param>
        public static void PrintInitialData(List<Info> data, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                // Write the table header
                writer.WriteLine("|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|");
                writer.WriteLine("| {0,-10} | {1,-15} | {2,-20} | {3,-35} | {4,-35} | {5,-20} | {6,-10} |", "Tema", "Sudėtingumas", "Autorius", "Klausimas", "Galimi atsakymai", "Teisingas atsakymas", "Taškai");
                writer.WriteLine("|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|");

                // Write each entry in the data
                foreach (Info info in data)
                {
                    // Combine answers into a single string
                    string possibleAnswers = string.Join(", ", info.Answers);
                    writer.WriteLine("| {0,-10} | {1,-15} | {2,-20} | {3,-35} | {4,-35} | {5,-20} | {6,-10} |",
                        info.Theme,
                        info.Difficulty,
                        info.Author,
                        info.Question,
                        possibleAnswers,
                        info.Correct,
                        info.Points);
                }

                writer.WriteLine("|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|");
            }
        }

        /// <summary>
        /// Writes the counts of different difficulty questions to the console.
        /// </summary>
        /// <param name="difficultyCounts">A dictionary with difficulty levels as keys and counts as values.</param>
        public static void PrintDifficultyCount(Dictionary<string, int> difficultyCounts)
        {
            Console.WriteLine("Skirtingo sudėtingumo klausimų skaičius:");
            foreach (var kvp in difficultyCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        /// <summary>
        /// Writes the unique themes to a specified CSV file.
        /// </summary>
        /// <param name="themes">HashSet containing unique themes.</param>
        /// <param name="fileName">The name of the file to write the themes.</param>
        public static void PrintUniqueThemes(HashSet<string> themes, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (string theme in themes)
                {
                    writer.WriteLine(theme); // Write each theme on a new line
                }
            }
        }

        /// <summary>
        /// Writes the selected questions to a specified CSV file in table format.
        /// </summary>
        /// <param name="questions">List of Info objects representing the questions.</param>
        /// <param name="fileName">The name of the file to write the questions.</param>
        public static void PrintQuestionPacket(List<Info> questions, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                // Write table header
                writer.WriteLine("|----------------------------------------------------------------------------------------------------------------|");
                writer.WriteLine("| {0,-10} | {1,-35} | {2,-50} | {3,-6} |", "Tema", "Klausimas", "Galimi atsakymai", "Taškai");
                writer.WriteLine("|----------------------------------------------------------------------------------------------------------------|");

                // Write each question with possible answers
                foreach (Info info in questions)
                {
                    string possibleAnswers = string.Join(", ", info.Answers);
                    writer.WriteLine("| {0,-10} | {1,-35} | {2,-50} | {3,-6} |",
                        info.Theme,
                        info.Question,
                        possibleAnswers,
                        info.Points);
                }

                writer.WriteLine("|----------------------------------------------------------------------------------------------------------------|");
            }
        }
    }
}

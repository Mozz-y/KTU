using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;

namespace Lab1.U17.Register
{
    static class TaskUtils
    {
        /// <summary>
        /// Print a table to a specified text file with values from the data.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        /// <param name="fileName">The name of the file to write the output.</param>
        public static void printInitialData(List<Info> data, string fileName)
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

        // Storage for printing
        private static int I; // Count of difficulty level I
        private static int II; // Count of difficulty level II
        private static int III; // Count of difficulty level III

        /// <summary>
        /// Count how many questions exist for each difficulty level.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        public static void countDifficulty(List<Info> data)
        {
            // Placeholder value
            I = 0;
            II = 0;
            III = 0;

            // Iterate through each Info object and count difficulties manually
            foreach (Info info in data)
            {
                if (info.Difficulty == "I")
                {
                    I++;
                }
                else if (info.Difficulty == "II")
                {
                    II++; 
                }
                else if (info.Difficulty == "III")
                {
                    III++; 
                }
            }
        }

        /// <summary>
        /// Print the counts of different difficulty questions
        /// </summary>
        public static void printDifficultyCount()
        {
            Console.WriteLine("Skirtingo sudėtingumo klausimų skaičius:");
            Console.WriteLine($"I: {I}");
            Console.WriteLine($"II: {II}");
            Console.WriteLine($"III: {III}");
        }

        /// <summary>
        /// Find unique themes from the list of Info objects.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        /// <returns>A HashSet containing unique themes.</returns>
        public static HashSet<string> uniqueThemeList(List<Info> data)
        {
            HashSet<string> uniqueThemes = new HashSet<string>();

            // Add each theme to the HashSet to ensure uniqueness
            foreach (Info info in data)
            {
                uniqueThemes.Add(info.Theme);
            }

            return uniqueThemes; // Return the unique themes
        }

        /// <summary>
        /// Write the unique themes to a specified CSV file.
        /// </summary>
        /// <param name="themes">HashSet containing unique themes.</param>
        /// <param name="fileName">The name of the file to write the themes.</param>
        public static void printUniqueThemes(HashSet<string> themes, string fileName)
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
        /// Create a packet of questions containing up to 4 unique themes.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        /// <returns>A List of Info objects representing the question packet.</returns>
        public static List<Info> questionPacket(List<Info> data)
        {
            HashSet<string> usedThemes = new HashSet<string>();
            List<Info> questionPacket = new List<Info>();

            foreach (Info info in data)
            {
                if (usedThemes.Count >= 4) break; // Stop if we have 4 unique themes
                if (!usedThemes.Contains(info.Theme))
                {
                    questionPacket.Add(info);
                    usedThemes.Add(info.Theme);
                }
            }

            return questionPacket;
        }

        /// <summary>
        /// Write the selected questions to a specified CSV file in table format.
        /// </summary>
        /// <param name="questions">List of Info objects representing the questions.</param>
        /// <param name="fileName">The name of the file to write the questions.</param>
        public static void printQuestionPacket(List<Info> questions, string fileName)
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

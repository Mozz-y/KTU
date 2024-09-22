using System;
using System.Collections.Generic;

namespace Lab1.U17.Register
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set console encoding to support special characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Path to the input CSV file
            string filePath = "data.csv";

            // Read data from the CSV file into a list of Info objects
            List<Info> data = InOutUtils.ReadData(filePath);

            // Output data from the initial CSV file to a text file
            TaskUtils.printInitialData(data, "poNuskaitymo.txt");

            // Count the number of questions for each difficulty level
            TaskUtils.countDifficulty(data);

            // Print the counts of questions for each difficulty level
            TaskUtils.printDifficultyCount();

            // Find unique themes from the list of questions
            HashSet<string> uniqueThemes = TaskUtils.uniqueThemeList(data);

            // Write the unique themes to a CSV file
            TaskUtils.printUniqueThemes(uniqueThemes, "Temos.csv");

            // Create a packet of questions containing up to 4 questions with unique themes
            List<Info> questionPacket = TaskUtils.questionPacket(data);

            // Write the selected questions to a CSV file in the specified table format
            TaskUtils.printQuestionPacket(questionPacket, "Klausimai.csv");
        }
    }
}

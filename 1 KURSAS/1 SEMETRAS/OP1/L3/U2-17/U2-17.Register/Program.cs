﻿using System;
using System.Collections.Generic;

namespace U2_17.Register
{
    class Program
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
            string duplicatesOutputFilePath = "VienodiKlausimai.csv";

            // Create an instance of task and load data
            EventRegister task = new EventRegister();
            EventRegister data = InOutUtils.ReadData(org1FilePath, org2FilePath); // Use EventRegister
            task.LoadData(data);

            // Output initial data
            InOutUtils.PrintInitialData(data, outputFilePath);

            // Output the most active authors
            Dictionary<string, List<(string Author, int Count)>> mostActiveAuthors = task.FindMostActiveAuthors();
            InOutUtils.PrintMostActiveAuthors(mostActiveAuthors);

            // Output the most difficult questions (not needed in U3)
            EventContainer mostDifficultQuestions = task.FindMostDifficultQuestions();
            InOutUtils.PrintMostDifficultQuestions(mostDifficultQuestions);

            // Output all themes
            List<string> allThemes = task.CollectAllThemes();
            InOutUtils.PrintAllThemes(allThemes, themesOutputFilePath);

            // Find questions that exist in both organizations
            EventContainer duplicates = task.FindDuplicateQuestions();

            // Sort duplicates by theme and author
            duplicates.SortDuplicates();

            // Output sorted duplicates to CSV file
            InOutUtils.PrintDuplicates(duplicates, duplicatesOutputFilePath);
        }
    }
}

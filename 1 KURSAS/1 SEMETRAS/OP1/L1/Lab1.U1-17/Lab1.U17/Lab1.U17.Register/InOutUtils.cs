using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab1.U17.Register
{
    static class InOutUtils
    {
        /// <summary>
        /// Read data from the specified CSV file and return a list of Info objects
        /// </summary>
        /// <param name="fileName">The path to the CSV file containing question data</param>
        /// <returns>A list of Info objects representing the questions from the CSV file</returns>
        public static List<Info> ReadData(string fileName)
        {
            List<Info> Data = new List<Info>();
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8); // Read all lines from the file

            foreach (string Line in Lines)
            {
                string[] Values = Line.Split(';'); // Split the line into values

                // Extract relevant information from the CSV format
                string theme = Values[0];
                string difficulty = Values[1];
                string author = Values[2];
                string question = Values[3];

                // Collect all possible answers in a list
                List<string> answers = new List<string>
                {
                    Values[4], Values[5], Values[6], Values[7]
                };

                string correctAnswer = Values[8]; // Correct answer
                int points = int.Parse(Values[9]); // Points for the question

                // Create an Info object and add it to the list
                Info info = new Info(theme, difficulty, author, question, answers, correctAnswer, points);
                Data.Add(info);
            }

            return Data; // Return the populated list of Info objects
        }
    }
}
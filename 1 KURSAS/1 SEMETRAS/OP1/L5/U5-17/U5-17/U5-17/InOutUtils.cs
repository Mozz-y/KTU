using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U5_17
{
    /// <summary>
    /// Provides utility methods for input/output operations, including reading data from files,
    /// writing formatted data to files, and displaying results in the console.
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Reads question data from a specified file and constructs a list of questions.
        /// </summary>
        /// <param name="filePath">The path to the file containing question data.</param>
        /// <returns>
        /// A tuple containing the organization name and a list of questions.
        /// </returns>
        public static (string OrganizationName, List<Question> Questions) ReadData(string filePath)
        {
            var questions = new List<Question>();
            string organizationName;

            using (var reader = new StreamReader(filePath))
            {
                // Read the first line as the organization name
                organizationName = reader.ReadLine();

                // Process the rest of the lines
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line into individual values
                    var values = line.Split(';');
                    string type = values[0]; // OPEN, TEST, MUSIC

                    // Determine the type of question and create an instance accordingly
                    switch (type)
                    {
                        case "OPEN":
                            questions.Add(new Question
                            {
                                Theme = values[1],
                                Difficulty = values[2],
                                Author = values[3],
                                QuestionText = values[4],
                                CorrectAnswer = values[5],
                                Points = int.Parse(values[6])
                            });
                            break;

                        case "TEST":
                            questions.Add(new TestQuestion
                            {
                                Theme = values[1],
                                Difficulty = values[2],
                                Author = values[3],
                                QuestionText = values[4],
                                CorrectAnswer = values[5],
                                Points = int.Parse(values[6]),
                                PossibleAnswers = new List<string> { values[7], values[8], values[9], values[10] }
                            });
                            break;

                        case "MUSIC":
                            questions.Add(new MusicQuestion
                            {
                                Theme = values[1],
                                Difficulty = values[2],
                                Author = values[3],
                                QuestionText = values[4],
                                CorrectAnswer = values[5],
                                Points = int.Parse(values[6]),
                                FileName = filePath
                            });
                            break;

                        default:
                            throw new InvalidOperationException($"Unknown question type: {type}");
                    }
                }
            }

            // Return the organization name and the list of questions
            return (organizationName, questions);
        }

        /// <summary>
        /// Writes a formatted table header to the provided StreamWriter.
        /// </summary>
        /// <param name="writer">The StreamWriter instance to write to.</param>
        private static void WriteTableHeader(StreamWriter writer)
        {
            writer.WriteLine(new string('-', 195));
            writer.WriteLine(
                $"{"Tema",-15} | {"Sudėtingumas",-12} | {"Autorius",-10} | {"Klausimo tekstas",-50} | {"Teisingas atsakymas",-25} | {"Taškai",-10} | {"Papildoma info",-40}");
            writer.WriteLine(new string('-', 195));
        }

        /// <summary>
        /// Writes question data to a specified file in a formatted table.
        /// </summary>
        /// <param name="organizationName">The name of the organization the questions belong to.</param>
        /// <param name="questions">A list of questions to be written.</param>
        /// <param name="filePath">The path to the file where the data should be written.</param>
        public static void PrintData(string organizationName, List<Question> questions, string filePath)
        {
            using (var writer = new StreamWriter(filePath, true, Encoding.UTF8)) // Append to the file
            {
                // Write the organization name as a section header
                writer.WriteLine($"\nOrganizacija: {organizationName}");
                writer.WriteLine(new string('=', 195));

                // Write the table header
                WriteTableHeader(writer);

                // Write each question as a row in the table
                foreach (var question in questions)
                {
                    writer.WriteLine(question.ToTableRow());
                }

                // Write a separator for better formatting
                writer.WriteLine(new string('-', 195));
            }
        }

        /// <summary>
        /// Prints the total counts of questions for each difficulty level to the console.
        /// </summary>
        /// <param name="counts">A tuple containing counts for I, II, and III difficulty levels.</param>
        public static void PrintTotalDifficulties((int I, int II, int III) counts)
        {
            Console.WriteLine($"I sudėtingumo: {counts.I}");
            Console.WriteLine($"II sudėtingumo: {counts.II}");
            Console.WriteLine($"III sudėtingumo: {counts.III}");
        }

        /// <summary>
        /// Prints the top authors for each organization to the console.
        /// </summary>
        /// <param name="topAuthors">
        /// A dictionary mapping organization names to their top authors and question counts.
        /// </param>
        public static void PrintTopAuthors(Dictionary<string, Dictionary<string, int>> topAuthors)
        {
            Console.WriteLine();
            Console.WriteLine("Aktyviausi autoriai kiekvienoje organizacijoje:");

            foreach (var org in topAuthors)
            {
                // Format and print the list of top authors for each organization
                string authors = string.Join(", ", org.Value.Select(a => $"{a.Key} - {a.Value}"));
                Console.WriteLine($"{org.Key}: {authors}");
            }
        }

        /// <summary>
        /// Writes the most difficult music questions to a specified file.
        /// </summary>
        /// <param name="questions">A list of music questions with difficulty III.</param>
        /// <param name="filePath">The path to the output file.</param>
        public static void PrintMostDifficultMusicQuestions(List<MusicQuestion> questions, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Write a header row
                writer.WriteLine("Sudėtingiausi muzikiniai klausimai");

                // Write each question's details
                foreach (var question in questions)
                {
                    writer.WriteLine($"{question.Theme}, {question.Difficulty}, {question.Author}, {question.QuestionText}, {question.CorrectAnswer}, {question.Points}, {question.FileName}");
                }
            }
        }

        /// <summary>
        /// Writes the most difficult general questions to a specified file.
        /// </summary>
        /// <param name="questions">A list of questions with difficulty III.</param>
        /// <param name="filePath">The path to the output file.</param>
        public static void PrintMostDifficultQuestions(List<Question> questions, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Write a header row
                writer.WriteLine("Sudėtingiausi klausimai");

                // Write each question's details
                foreach (var question in questions)
                {
                    writer.WriteLine($"{question.Theme}, {question.Difficulty}, {question.Author}, {question.QuestionText}, {question.CorrectAnswer}, {question.Points}, {question.GetAdditionalInfo()}");
                }
            }
        }

        /// <summary>
        /// Writes all "Linksmasis"-themed questions to a specified file, sorted by author and points.
        /// </summary>
        /// <param name="questions">A list of questions with the "Linksmasis" theme.</param>
        /// <param name="filePath">The path to the output file.</param>
        public static void PrintFunQuestions(List<Question> questions, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8)) // Overwrite file
            {
                // Write a descriptive header
                writer.WriteLine("Klausimai, kurių tema yra linksmasis išrikiuoti pagal autorių ir taškus");

                // Write each question to the file
                foreach (var question in questions)
                {
                    writer.WriteLine($"{question.Theme}, {question.Difficulty}, {question.Author}, {question.QuestionText}, {question.CorrectAnswer}, {question.Points}, {question.GetAdditionalInfo()}");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Lab1.U17.Register
{
    class Info
    {
        /// <summary>
        /// Properties for the Info class with public getters and setters
        /// </summary>
        public string Theme { get; set; }
        public string Difficulty { get; set; }
        public string Author { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public string Correct { get; set; }
        public int Points { get; set; }

        /// <summary>
        /// Constructor to initialize an Info object with provided values
        /// </summary>
        /// <param name="theme">The theme of the question</param>
        /// <param name="difficulty">The difficulty level of the question (I, II, III)</param>
        /// <param name="author">The author of the question</param>
        /// <param name="question">The question text</param>
        /// <param name="answers">A list of possible answers</param>
        /// <param name="correctAnswer">The correct answer from the list of possible answers</param>
        /// <param name="points">The points awarded for answering the question correctly</param>
        public Info(string theme, string difficulty, string author, string question, List<string> answers, string correctAnswer, int points)
        {
            Theme = theme;
            Difficulty = difficulty;
            Author = author;
            Question = question;
            Answers = answers;
            Correct = correctAnswer;
            Points = points;
        }
    }
}
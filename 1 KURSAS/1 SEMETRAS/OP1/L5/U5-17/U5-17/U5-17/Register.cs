using System.Collections.Generic;
using System.Linq;

namespace U5_17
{
    public class Register
    {
        private readonly string _organizationName;  // Holds the organization name
        private readonly List<Question> _questions;  // Holds the list of questions

        /// <summary>
        /// Initializes a new instance of the <see cref="Register"/> class with the specified organization name and list of questions.
        /// </summary>
        /// <param name="organizationName">The name of the organization.</param>
        /// <param name="questions">The list of questions for the register.</param>
        public Register(string organizationName, List<Question> questions)
        {
            _organizationName = organizationName;  // Set the organization name
            _questions = questions;  // Set the questions list
        }

        /// <summary>
        /// Gets the name of the organization.
        /// </summary>
        /// <returns>The name of the organization.</returns>
        public string GetOrganizationName() => _organizationName;

        /// <summary>
        /// Gets the list of questions in the register.
        /// </summary>
        /// <returns>A list of questions.</returns>
        public List<Question> GetQuestions() => _questions;

        /// <summary>
        /// Gets the total number of questions in the register.
        /// </summary>
        /// <returns>The total count of questions.</returns>
        public int GetCount() => _questions.Count;

        /// <summary>
        /// Gets the question entry at the specified index.
        /// </summary>
        /// <param name="index">The index of the question.</param>
        /// <returns>The question at the specified index.</returns>
        public Question GetEntry(int index) => _questions[index];

        /// <summary>
        /// Finds the total counts of questions with each difficulty level across three registers.
        /// </summary>
        /// <param name="second">The second register to consider.</param>
        /// <param name="third">The third register to consider.</param>
        /// <returns>A tuple containing the total counts of difficulty levels I, II, and III.</returns>
        public (int I, int II, int III) FindTotalDifficulties(Register second, Register third)
        {
            int totalI = this._questions.Count(q => q.Difficulty == "I") +  // Count difficulty "I" questions
                         second._questions.Count(q => q.Difficulty == "I") +  // Count difficulty "I" questions from second register
                         third._questions.Count(q => q.Difficulty == "I");  // Count difficulty "I" questions from third register

            int totalII = this._questions.Count(q => q.Difficulty == "II") +  // Count difficulty "II" questions
                          second._questions.Count(q => q.Difficulty == "II") +  // Count difficulty "II" questions from second register
                          third._questions.Count(q => q.Difficulty == "II");  // Count difficulty "II" questions from third register

            int totalIII = this._questions.Count(q => q.Difficulty == "III") +  // Count difficulty "III" questions
                           second._questions.Count(q => q.Difficulty == "III") +  // Count difficulty "III" questions from second register
                           third._questions.Count(q => q.Difficulty == "III");  // Count difficulty "III" questions from third register

            return (totalI, totalII, totalIII);  // Return the counts for all difficulty levels
        }

        /// <summary>
        /// Gets the author(s) with the most questions in the register.
        /// </summary>
        /// <returns>A dictionary where the key is the author and the value is the number of questions they have.</returns>
        public Dictionary<string, int> GetTopAuthors()
        {
            var groupedAuthors = _questions  // Group questions by their authors
                .GroupBy(q => q.Author)
                .Select(group => new { Author = group.Key, Count = group.Count() })  // Select author and count of their questions
                .ToList();

            int maxQuestions = groupedAuthors.Max(x => x.Count);  // Find the maximum count of questions

            // Get only one author with the highest count
            var topAuthor = groupedAuthors
                .Where(x => x.Count == maxQuestions)  // Filter authors who have the max number of questions
                .Take(1)  // Take only the first author if there are ties
                .ToDictionary(x => x.Author, x => x.Count);  // Convert the result into a dictionary

            return topAuthor;  // Return the top author(s) as a dictionary
        }

        /// <summary>
        /// Finds the most difficult music questions across three registers.
        /// </summary>
        /// <param name="second">The second register to consider.</param>
        /// <param name="third">The third register to consider.</param>
        /// <returns>A list of the most difficult music questions.</returns>
        public List<MusicQuestion> FindDifficultMusicQuestions(Register second, Register third)
        {
            var allMusicQuestions = this._questions.OfType<MusicQuestion>()  // Get music questions from the current register
                .Concat(second._questions.OfType<MusicQuestion>())  // Concatenate music questions from the second register
                .Concat(third._questions.OfType<MusicQuestion>());  // Concatenate music questions from the third register

            if (allMusicQuestions.Any(mq => mq.Difficulty == "III"))  // Check if there are any difficulty III music questions
                return allMusicQuestions.Where(mq => mq.Difficulty == "III").ToList();  // Return difficulty III questions
            else if (allMusicQuestions.Any(mq => mq.Difficulty == "II"))  // Check if there are any difficulty II music questions
                return allMusicQuestions.Where(mq => mq.Difficulty == "II").ToList();  // Return difficulty II questions
            else  // If no difficulty III or II, return difficulty I questions
                return allMusicQuestions.Where(mq => mq.Difficulty == "I").ToList();
        }

        /// <summary>
        /// Finds the most difficult questions across three registers.
        /// </summary>
        /// <param name="second">The second register to consider.</param>
        /// <param name="third">The third register to consider.</param>
        /// <returns>A list of the most difficult questions.</returns>
        public List<Question> FindAllDifficultQuestions(Register second, Register third)
        {
            var allQuestions = this._questions  // Get all questions from the current register
                .Concat(second._questions)  // Concatenate questions from the second register
                .Concat(third._questions);  // Concatenate questions from the third register

            if (allQuestions.Any(q => q.Difficulty == "III"))  // Check if there are any difficulty III questions
                return allQuestions.Where(q => q.Difficulty == "III").ToList();  // Return difficulty III questions
            else if (allQuestions.Any(q => q.Difficulty == "II"))  // Check if there are any difficulty II questions
                return allQuestions.Where(q => q.Difficulty == "II").ToList();  // Return difficulty II questions
            else  // If no difficulty III or II, return difficulty I questions
                return allQuestions.Where(q => q.Difficulty == "I").ToList();
        }

        /// <summary>
        /// Finds all "Linksmasis" themed questions across three registers and orders them by author and points.
        /// </summary>
        /// <param name="second">The second register to consider.</param>
        /// <param name="third">The third register to consider.</param>
        /// <returns>A list of "Linksmasis" themed questions ordered by author and points.</returns>
        public List<Question> GetFunQuestions(Register second, Register third)
        {
            return this._questions  // Start with the questions in the current register
                .Concat(second._questions)  // Concatenate questions from the second register
                .Concat(third._questions)  // Concatenate questions from the third register
                .Where(q => q.Theme == "Linksmasis")  // Filter by the theme "Linksmasis"
                .OrderBy(q => q.Author)  // Order by author name
                .ThenByDescending(q => q.Points)  // Then order by points in descending order
                .ToList();  // Convert the result into a list
        }
    }
}

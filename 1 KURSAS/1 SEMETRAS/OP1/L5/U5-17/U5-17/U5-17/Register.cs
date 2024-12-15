using System.Collections.Generic;
using System.Linq;

namespace U5_17
{
    /// <summary>
    /// The Register class is responsible for data manipulation and operations with questions
    /// from three organizations.
    /// </summary>
    public class Register
    {
        // Private fields to hold the questions from each organization
        private readonly List<Question> _organization1Questions;
        private readonly List<Question> _organization2Questions;
        private readonly List<Question> _organization3Questions;

        /// <summary>
        /// Constructor that initializes the Register with questions from three organizations.
        /// </summary>
        /// <param name="questions1">List of questions from the first organization.</param>
        /// <param name="questions2">List of questions from the second organization.</param>
        /// <param name="questions3">List of questions from the third organization.</param>
        public Register(List<Question> questions1, List<Question> questions2, List<Question> questions3)
        {
            // Initialize fields with questions from each organization
            _organization1Questions = questions1;
            _organization2Questions = questions2;
            _organization3Questions = questions3;
        }

        /// <summary>
        /// Computes the total number of questions for each difficulty level across all organizations.
        /// </summary>
        /// <returns>
        /// A tuple containing counts for I, II, and III difficulty levels.
        /// </returns>
        public (int I, int II, int III) ComputeTotalDifficulties()
        {
            // Count questions with difficulty "I" across all organizations
            int totalI = _organization1Questions.Count(q => q.Difficulty == "I")
                         + _organization2Questions.Count(q => q.Difficulty == "I")
                         + _organization3Questions.Count(q => q.Difficulty == "I");

            // Count questions with difficulty "II" across all organizations
            int totalII = _organization1Questions.Count(q => q.Difficulty == "II")
                          + _organization2Questions.Count(q => q.Difficulty == "II")
                          + _organization3Questions.Count(q => q.Difficulty == "II");

            // Count questions with difficulty "III" across all organizations
            int totalIII = _organization1Questions.Count(q => q.Difficulty == "III")
                           + _organization2Questions.Count(q => q.Difficulty == "III")
                           + _organization3Questions.Count(q => q.Difficulty == "III");

            // Return the counts as a tuple
            return (totalI, totalII, totalIII);
        }

        /// <summary>
        /// Private helper to find the top authors for a given list of questions.
        /// </summary>
        /// <param name="questions">List of questions from an organization.</param>
        /// <returns>
        /// A dictionary with authors as keys and their respective question counts as values.
        /// </returns>
        private Dictionary<string, int> FindTopAuthors(List<Question> questions)
        {
            // Group questions by author and calculate the number of questions each author created
            var groupedAuthors = questions
                .GroupBy(q => q.Author) // Group questions by author
                .Select(group => new { Author = group.Key, Count = group.Count() }) // Project author and question count
                .ToList();

            // Find the maximum number of questions created by a single author
            int maxQuestions = groupedAuthors.Max(x => x.Count);

            // Select authors who created the maximum number of questions
            var topAuthors = groupedAuthors
                .Where(x => x.Count == maxQuestions) // Filter for top authors
                .ToDictionary(x => x.Author, x => x.Count); // Convert to dictionary

            return topAuthors;
        }

        /// <summary>
        /// Finds the top authors for each organization.
        /// </summary>
        /// <param name="orgData">A dictionary mapping organization names to their question lists.</param>
        /// <returns>
        /// A dictionary mapping organization names to dictionaries of top authors and their question counts.
        /// </returns>
        public Dictionary<string, Dictionary<string, int>> GetTopAuthors(Dictionary<string, List<Question>> orgData)
        {
            // Map each organization name to its top authors using the helper method
            return orgData.ToDictionary(
                kvp => kvp.Key, // Organization name
                kvp => FindTopAuthors(kvp.Value) // Find top authors for that organization
            );
        }

        /// <summary>
        /// Finds all music questions with difficulty level III across all organizations.
        /// </summary>
        /// <returns>A list of difficulty III music questions.</returns>
        public List<MusicQuestion> FindDifficultMusicQuestions()
        {
            // Combine all questions from all three organizations
            var allQuestions = _organization1Questions
                .Concat(_organization2Questions) // Combine Org1 and Org2 questions
                .Concat(_organization3Questions) // Add Org3 questions
                .OfType<MusicQuestion>() // Select only MusicQuestion instances
                .Where(mq => mq.Difficulty == "III") // Filter by difficulty "III"
                .ToList();

            return allQuestions; // Return the filtered list
        }

        /// <summary>
        /// Finds all questions with difficulty level III across all organizations.
        /// </summary>
        /// <returns>A list of difficulty III questions.</returns>
        public List<Question> FindAllDifficultyIIIQuestions()
        {
            // Combine all questions from all three organizations
            var allQuestions = _organization1Questions
                .Concat(_organization2Questions) // Combine Org1 and Org2 questions
                .Concat(_organization3Questions) // Add Org3 questions
                .Where(q => q.Difficulty == "III") // Filter for difficulty "III"
                .ToList();

            return allQuestions; // Return the filtered list
        }

        /// <summary>
        /// Finds all questions with the theme "Linksmasis" and sorts them by author and points.
        /// </summary>
        /// <returns>A sorted list of "Linksmasis"-themed questions.</returns>
        public List<Question> GetFunQuestions()
        {
            // Combine all questions from all three organizations
            return _organization1Questions.Concat(_organization2Questions) // Combine Org1 and Org2 questions
                     .Concat(_organization3Questions) // Add Org3 questions
                     .Where(q => q.Theme == "Linksmasis") // Filter for theme "Linksmasis"
                     .OrderBy(q => q.Author) // Sort alphabetically by author
                     .ThenByDescending(q => q.Points) // Then sort by points (descending)
                     .ToList(); // Convert to a list
        }
    }
}

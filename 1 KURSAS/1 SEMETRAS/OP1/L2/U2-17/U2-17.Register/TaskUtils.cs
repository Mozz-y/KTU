using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace U2_17.Register
{
    public class TaskUtils
    {
        private List<Info> _infoList; // Private list to store Info objects

        // Constructor to initialize the private list
        public TaskUtils(List<Info> infoList)
        {
            _infoList = infoList; // Initialize the private list
        }

        /// <summary>
        /// Finds the top authors in each organization and their question counts.
        /// </summary>
        /// <returns>A dictionary containing organizations as keys and their top authors with counts as values.</returns>
        public Dictionary<string, List<(string Author, int Count)>> FindMostActiveAuthors()
        {
            var authorCount = CountQuestionsByAuthorsInOrg(); // Count authors' questions
            return MostActiveAuthors(authorCount); // Get top authors based on counts
        }

        /// <summary>
        /// Counts the number of questions for each author in each organization.
        /// </summary>
        /// <returns>A dictionary with organizations as keys and another dictionary as values containing authors and their counts.</returns>
        private Dictionary<string, Dictionary<string, int>> CountQuestionsByAuthorsInOrg()
        {
            var authorCount = new Dictionary<string, Dictionary<string, int>>();

            foreach (var info in _infoList)
            {
                if (!authorCount.ContainsKey(info.Organization))
                {
                    authorCount[info.Organization] = new Dictionary<string, int>();
                }

                // Count questions for each author
                if (authorCount[info.Organization].ContainsKey(info.Author))
                {
                    authorCount[info.Organization][info.Author]++;
                }
                else
                {
                    authorCount[info.Organization][info.Author] = 1;
                }
            }

            return authorCount;
        }

        /// <summary>
        /// Gets the top authors based on the counts obtained from CountAuthors method.
        /// </summary>
        /// <param name="authorCount">A dictionary containing authors and their counts for each organization.</param>
        /// <returns>A dictionary with organizations as keys and their top authors with counts as values.</returns>
        private Dictionary<string, List<(string Author, int Count)>> MostActiveAuthors(Dictionary<string, Dictionary<string, int>> authorCount)
        {
            var topAuthorsByOrg = new Dictionary<string, List<(string Author, int Count)>>();

            foreach (var org in authorCount.Keys)
            {
                int maxCount = 0;
                var topAuthors = new List<(string Author, int Count)>();

                // Find the maximum count for the current organization
                foreach (var author in authorCount[org])
                {
                    if (author.Value > maxCount)
                    {
                        maxCount = author.Value;
                        topAuthors.Clear(); // Clear previous top authors
                        topAuthors.Add((author.Key, author.Value));
                    }
                    else if (author.Value == maxCount)
                    {
                        topAuthors.Add((author.Key, author.Value)); // Add another top author
                    }
                }

                topAuthorsByOrg[org] = topAuthors; // Store the top authors for the organization
            }

            return topAuthorsByOrg;
        }

        public static List<Info> FindMostDifficultQuestions(List<Info> data)
        {
            // This method should take in List<Info> and implement logic to find the most difficult questions.
            List<Info> mostDifficultQuestions = new List<Info>();

            // Assuming the difficulty level "III" is the most difficult
            foreach (var info in data)
            {
                if (info.Difficulty.Equals("III"))
                {
                    mostDifficultQuestions.Add(info);
                }
            }

            return mostDifficultQuestions;
        }

        // Method to collect all themes from both organizations
        public List<string> CollectAllThemes()
        {
            List<string> themes = new List<string>();

            // Loop through the data and collect all themes
            foreach (var info in _infoList)
            {
                themes.Add(info.Theme);  // Add each theme (duplicates included)
            }

            return themes;
        }
    }
}

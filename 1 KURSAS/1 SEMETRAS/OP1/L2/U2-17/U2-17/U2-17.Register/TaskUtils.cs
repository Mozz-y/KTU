using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace U2_17.Register
{
    public class TaskUtils
    {
        private List<Info> _infoList; // Private list to store Info objects

        // Constructor to initialize the private list
        public TaskUtils()
        {
            _infoList = new List<Info>();
        }

        /// <summary>
        /// Adds a question (Info object) to the internal list.
        /// </summary>
        /// <param name="question">The Info object to add.</param>
        public void Add(Info question)
        {
            _infoList.Add(question);
        }

        /// <summary>
        /// Processes the loaded data from the given files, adding them to the list.
        /// </summary>
        public void LoadData(List<Info> data)
        {
            foreach (Info info in data)
            {
                Add(info);
            }
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

        /// <summary>
        /// Finds the most difficult questions.
        /// </summary>
        public List<Info> FindMostDifficultQuestions()
        {
            var mostDifficultQuestions = new List<Info>();

            foreach (Info info in _infoList)
            {
                if (info == "III")
                {
                    mostDifficultQuestions.Add(info);
                }
            }

            return mostDifficultQuestions;
        }

        // Method to collect all themes from both organizations
        public List<string> CollectAllThemes()
        {
            // Use a HashSet to automatically handle duplicates
            var themes = new HashSet<string>();

            // Loop through the data and collect unique themes
            foreach (Info info in _infoList)
            {
                themes.Add(info.Theme);  // Add each theme (duplicates are automatically ignored)
            }

            // Convert the HashSet back to a List
            return themes.ToList();
        }
    }
}

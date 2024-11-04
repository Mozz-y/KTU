using System.Collections.Generic;
using System.Linq;

namespace U2_17.Register
{
    public class EventRegister
    {
        private EventContainer _eventContainer;

        // Constructor to initialize the private container
        public EventRegister()
        {
            _eventContainer = new EventContainer();
        }

        /// <summary>
        /// Adds a question (Info object) to the internal container.
        /// </summary>
        public void Add(Info info)
        {
            _eventContainer.Add(info);
        }

        /// <summary>
        /// Processes the loaded data, adding each item to the container.
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
        public Dictionary<string, List<(string Author, int Count)>> FindMostActiveAuthors()
        {
            var authorCount = CountQuestionsByAuthorsInOrg(); // Count authors' questions
            return MostActiveAuthors(authorCount); // Get top authors based on counts
        }

        /// <summary>
        /// Counts the number of questions for each author in each organization.
        /// </summary>
        private Dictionary<string, Dictionary<string, int>> CountQuestionsByAuthorsInOrg()
        {
            var authorCount = new Dictionary<string, Dictionary<string, int>>();

            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);

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
        private Dictionary<string, List<(string Author, int Count)>> MostActiveAuthors(Dictionary<string, Dictionary<string, int>> authorCount)
        {
            var topAuthorsByOrg = new Dictionary<string, List<(string Author, int Count)>>();

            foreach (var org in authorCount.Keys)
            {
                int maxCount = 0;
                var topAuthors = new List<(string Author, int Count)>();

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

            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                if (info.Difficulty == "III")
                {
                    mostDifficultQuestions.Add(info);
                }
            }

            return mostDifficultQuestions;
        }

        /// <summary>
        /// Collects all unique themes from the questions.
        /// </summary>
        public List<string> CollectAllThemes()
        {
            var themes = new HashSet<string>();

            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                themes.Add(info.Theme);
            }

            return new List<string>(themes);
        }

        public List<string> GetUniqueOrganizations()
        {
            var organizations = new HashSet<string>();
            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                organizations.Add(info.Organization);
            }
            return organizations.ToList();
        }

        public List<Info> FindDuplicateQuestions()
        {
            var organizations = GetUniqueOrganizations();

            if (organizations.Count < 2)
            {
                // Not enough organizations to compare
                return new List<Info>();
            }

            var questionsOrg1 = new HashSet<string>();
            var duplicates = new List<Info>();

            // Collect unique questions from the first organization
            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                if (info.Organization == organizations[0]) // Use the first organization
                {
                    questionsOrg1.Add(info.Question);
                }
            }

            // Check questions from the second organization against the first
            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                if (info.Organization == organizations[1] && questionsOrg1.Contains(info.Question))
                {
                    duplicates.Add(info); // Add to duplicates if found in the first organization
                }
            }

            return duplicates; // Return unsorted duplicates
        }

        public void SortEventContainer(List<Info> duplicates)
        {
            _eventContainer.SortDuplicates(duplicates);
        }

    }
}

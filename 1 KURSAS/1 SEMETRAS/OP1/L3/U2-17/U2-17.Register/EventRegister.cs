using System;
using System.Collections.Generic;
using System.Linq;

namespace U2_17.Register
{
    class EventRegister
    {
        private EventContainer _eventContainer;

        // Constructor to initialize the internal container
        public EventRegister()
        {
            _eventContainer = new EventContainer();
        }

        /// <summary>
        /// Adds an Info object to the container.
        /// </summary>
        public void Add(Info info)
        {
            _eventContainer.Add(info);
        }

        public int GetCount()
        {
            return _eventContainer.Count;
        }

        public Info GetEntry(int index)
        {
            return _eventContainer.Get(index);
        }

        /// <summary>
        /// Loads multiple Info objects into the container.
        /// </summary>
        public void LoadData(EventRegister data)
        {
            // Iterate over the EventRegister
            for (int i = 0; i < data.GetCount(); i++)
            {
                var info = data.GetEntry(i); // Get the info from EventRegister
                _eventContainer.Add(info);  // Add it to the private container
            }
        }

        /// <summary>
        /// Finds the most active authors by counting their contributions per organization.
        /// </summary>
        public Dictionary<string, List<(string Author, int Count)>> FindMostActiveAuthors()
        {
            var authorCount = CountQuestionsByAuthorsInOrg();
            return MostActiveAuthors(authorCount);
        }

        /// <summary>
        /// Counts the questions by each author in each organization.
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
        /// Returns the top authors based on their contribution count.
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
                        topAuthors.Clear();
                        topAuthors.Add((author.Key, author.Value));
                    }
                    else if (author.Value == maxCount)
                    {
                        topAuthors.Add((author.Key, author.Value));
                    }
                }

                topAuthorsByOrg[org] = topAuthors;
            }

            return topAuthorsByOrg;
        }

        /// <summary>
        /// Returns the most difficult questions (those with difficulty "III").
        /// </summary>
        public EventContainer FindMostDifficultQuestions()
        {
            var difficultQuestions = new EventContainer();

            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                if (info.Difficulty == "III")
                {
                    difficultQuestions.Add(info);
                }
            }

            return difficultQuestions;
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

            return themes.ToList();
        }

        /// <summary>
        /// Collects all unique organizations from the questions.
        /// </summary>
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

        /// <summary>
        /// Finds duplicate questions between two organizations.
        /// </summary>
        public EventContainer FindDuplicateQuestions()
        {
            var organizations = GetUniqueOrganizations();

            if (organizations.Count < 2)
            {
                return new EventContainer();
            }

            var questionsOrg1 = new HashSet<string>();
            var duplicates = new EventContainer();

            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                if (info.Organization == organizations[0]) 
                {
                    questionsOrg1.Add(info.Question);
                }
            }

            for (int i = 0; i < _eventContainer.Count; i++)
            {
                var info = _eventContainer.Get(i);
                if (info.Organization == organizations[1] && questionsOrg1.Contains(info.Question))
                {
                    duplicates.Add(info);
                }
            }

            return duplicates;
        }
    }
}

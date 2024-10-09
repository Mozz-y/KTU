using System;
using System.Collections.Generic;
using System.IO;

namespace Lab1.U17.Register
{
    static class TaskUtils
    {
        /// <summary>
        /// Counts how many questions exist for each difficulty level.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        /// <returns>A dictionary with difficulty levels as keys and counts as values.</returns>
        public static Dictionary<string, int> CountDifficulty(List<Info> data)
        {
            // Initialize the dictionary with all difficulty levels set to zero
            Dictionary<string, int> difficultyCounts = new Dictionary<string, int> { { "I", 0 }, { "II", 0 }, { "III", 0 } };

            // Iterate through the data and count difficulties
            foreach (Info info in data)
            {
                if (difficultyCounts.ContainsKey(info.Difficulty))
                {
                    difficultyCounts[info.Difficulty]++;
                }
                else
                {
                    // Optionally handle unexpected difficulty levels
                    //Console.WriteLine($"Šis pateiktas sudėtingumo lygis negalimas - '{info.Difficulty}', pasirinkite tarp I, II ir III.");
                }
            }

            return difficultyCounts;
        }

        /// <summary>
        /// Find unique themes from the list of Info objects.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        /// <returns>A HashSet containing unique themes.</returns>
        public static HashSet<string> UniqueThemeList(List<Info> data)
        {
            HashSet<string> uniqueThemes = new HashSet<string>();

            // Add each theme to the HashSet to ensure uniqueness
            foreach (Info info in data)
            {
                uniqueThemes.Add(info.Theme);
            }

            return uniqueThemes; // Return the unique themes
        }

        /// <summary>
        /// Create a packet of questions containing up to 4 unique themes.
        /// </summary>
        /// <param name="data">List of Info objects containing question data.</param>
        /// <returns>A List of Info objects representing the question packet.</returns>
        public static List<Info> QuestionPacket(List<Info> data)
        {
            HashSet<string> usedThemes = new HashSet<string>();
            List<Info> questionPacket = new List<Info>();

            foreach (Info info in data)
            {
                if (usedThemes.Count >= 4) break; // Stop if we have 4 unique themes
                if (!usedThemes.Contains(info.Theme))
                {
                    questionPacket.Add(info);
                    usedThemes.Add(info.Theme);
                }
            }

            return questionPacket;
        }
    }
}

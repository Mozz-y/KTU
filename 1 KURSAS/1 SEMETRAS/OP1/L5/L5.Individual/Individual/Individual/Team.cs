using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public class Team
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string CoachName { get; set; }
        public int GamesPlayed { get; set; }

        public Team(string name, string city, string coachName, int gamesPlayed)
        {
            Name = name;
            City = city;
            CoachName = coachName;
            GamesPlayed = gamesPlayed;
        }
    }
}

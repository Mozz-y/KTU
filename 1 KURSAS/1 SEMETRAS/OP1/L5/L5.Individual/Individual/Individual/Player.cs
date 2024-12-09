using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public abstract class Player
    {
        public string TeamName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GamesPlayed { get; set; }
        public int Score { get; set; }

        protected Player(string teamName, string lastName, string firstName, DateTime birthDate, int gamesPlayed, int score)
        {
            TeamName = teamName;
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
            GamesPlayed = gamesPlayed;
            Score = score;
        }
    }
}

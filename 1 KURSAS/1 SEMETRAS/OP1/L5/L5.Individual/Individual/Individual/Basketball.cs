using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public class Basketball : Player
    {
        public int Rebounds { get; set; }
        public int Assists { get; set; }

        public Basketball(string teamName, string lastName, string firstName, DateTime birthDate, int gamesPlayed, int score, int rebounds, int assists)
            : base(teamName, lastName, firstName, birthDate, gamesPlayed, score)
        {
            Rebounds = rebounds;
            Assists = assists;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public class Football : Player
    {
        public int YellowCards { get; set; }

        public Football(string teamName, string lastName, string firstName, DateTime birthDate, int gamesPlayed, int score, int yellowCards)
            : base(teamName, lastName, firstName, birthDate, gamesPlayed, score)
        {
            YellowCards = yellowCards;
        }
    }
}

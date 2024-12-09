using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public class Calculations
    {
        private readonly List<Basketball> _basketballPlayers;
        private readonly List<Football> _footballPlayers;

        public Calculations(List<Basketball> basketballPlayers, List<Football> footballPlayers)
        {
            _basketballPlayers = basketballPlayers;
            _footballPlayers = footballPlayers;
        }

        public double CalculateAverageBasketballPoints()
        {
            if (_basketballPlayers.Count == 0) return 0;
            return _basketballPlayers.Average(player => player.Score);
        }

        public double CalculateAverageFootballGoals()
        {
            if (_footballPlayers.Count == 0) return 0;
            return _footballPlayers.Average(player => player.Score);
        }

        public List<Basketball> GetBasketballPlayersFromCity(string city, int gamesPlayed)
        {
            return _basketballPlayers
                .Where(player => player.TeamName.Equals(city, StringComparison.OrdinalIgnoreCase) && player.GamesPlayed == gamesPlayed)
                .ToList();
        }

        public List<Football> GetFootballPlayersFromCity(string city, int gamesPlayed)
        {
            return _footballPlayers
                .Where(player => player.TeamName.Equals(city, StringComparison.OrdinalIgnoreCase) && player.GamesPlayed == gamesPlayed)
                .ToList();
        }
    }

}

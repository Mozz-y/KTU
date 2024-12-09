using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public class Register
    {
        private readonly SportsContainer _container;

        private double _averageBasketballScore;
        private double _averageFootballGoals;

        public Register(SportsContainer container)
        {
            _container = container;
            CalculateAverages();
        }

        public void CalculateAverages()
        {
            if (_container.Basketball.Any())
                _averageBasketballScore = _container.BasketballPlayers.Average(p => p.Points);

            if (_container.FootballPlayers.Any())
                _averageFootballGoals = _container.FootballPlayers.Average(p => p.Goals);
        }

        public List<Player> GetPlayersFromCity(string city)
        {
            var filteredPlayers = new List<Player>();

            // Filter basketball players meeting conditions
            foreach (var player in _container.BasketballPlayers)
            {
                if (_container.Teams.Any(t => t.City == city && t.GamesPlayed == player.GamesPlayed) &&
                    player.Points >= _averageBasketballScore)
                {
                    filteredPlayers.Add(player);
                }
            }

            // Filter football players meeting conditions
            foreach (var player in _container.FootballPlayers)
            {
                if (_container.Teams.Any(t => t.City == city && t.GamesPlayed == player.GamesPlayed) &&
                    player.Goals >= _averageFootballGoals)
                {
                    filteredPlayers.Add(player);
                }
            }

            return filteredPlayers;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public class SportsContainer
    {
        private readonly List<Basketball> _Basketballs;
        private readonly List<Football> _Footballs;
        private readonly List<Team> _teams;

        public SportsContainer()
        {
            _Basketballs = new List<Basketball>();
            _Footballs = new List<Football>();
            _teams = new List<Team>();
        }

        public void AddBasketballPlayer(Basketball player)
        {
            _Basketballs.Add(player);
        }

        public void AddFootballPlayer(Football player)
        {
            _Footballs.Add(player);
        }

        public void AddTeam(Team team)
        {
            _teams.Add(team);
        }

        public List<Team> GetTeams()
        {
            return _teams;
        }

        public List<Basketball> GetBasketballPlayers()
        {
            return _Basketballs;
        }

        public List<Football> GetFootballPlayers()
        {
            return _Footballs;
        }

        /// <summary>
        /// Get the number of games played for a given team's name/city.
        /// </summary>
        public int GetTeamGamesPlayed(string city)
        {
            var team = _teams.FirstOrDefault(t => t.City.Equals(city, StringComparison.OrdinalIgnoreCase));
            return team?.GamesPlayed ?? 0;
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "input.txt";
            var sportsContainer = InOutUtils.ReadDataFromFile(fileName);
            var calculations = new Calculations(
                sportsContainer.GetBasketballPlayers(),
                sportsContainer.GetFootballPlayers()
            );

            // Calculate averages
            double averageBasketballPoints = calculations.CalculateAverageBasketballPoints();
            double averageFootballGoals = calculations.CalculateAverageFootballGoals();


            // Ask user to choose a city/team
            InOutUtils.PrintTeams(sportsContainer.GetTeams());

            string selectedCity = Console.ReadLine();

            var basketballPlayers = calculations.GetBasketballPlayersFromCity(selectedCity, sportsContainer.GetTeamGamesPlayed(selectedCity));
            var footballPlayers = calculations.GetFootballPlayersFromCity(selectedCity, sportsContainer.GetTeamGamesPlayed(selectedCity));

            InOutUtils.PrintPlayers(basketballPlayers, averageBasketballPoints);
            InOutUtils.PrintPlayers(footballPlayers, averageFootballGoals);

        }
    }

}

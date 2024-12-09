using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Individual
{
    public static class InOutUtils
    {
        public static SportsContainer ReadDataFromFile(string fileName)
        {
            SportsContainer sportsContainer = new SportsContainer();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);

            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                string type = values[0];

                switch (type.ToUpper())
                {
                    case "BASKETBALL":
                        // Handle Basketball player parsing
                        if (values.Length >= 7)
                        {
                            string teamName = values[1];
                            string lastName = values[2];
                            string firstName = values[3];
                            DateTime birthDate = DateTime.Parse(values[4]);
                            int gamesPlayed = int.Parse(values[5]);
                            int score = int.Parse(values[6]);
                            int rebounds = int.Parse(values[7]);
                            int assists = int.Parse(values[8]);
                            Basketball basketballPlayer = new Basketball(
                                teamName,
                                lastName,
                                firstName,
                                birthDate,
                                gamesPlayed,
                                score,
                                rebounds,
                                assists
                            );
                            sportsContainer.AddBasketballPlayer(basketballPlayer);
                        }
                        break;

                    case "FOOTBALL":
                        // Handle Football player parsing
                        if (values.Length >= 6)
                        {
                            string teamName = values[1];
                            string lastName = values[2];
                            string firstName = values[3];
                            DateTime birthDate = DateTime.Parse(values[4]);
                            int gamesPlayed = int.Parse(values[5]);
                            int score = int.Parse(values[6]);
                            int yellowCards = int.Parse(values[7]);
                            Football footballPlayer = new Football(
                                teamName,
                                lastName,
                                firstName,
                                birthDate,
                                gamesPlayed,
                                score,
                                yellowCards
                            );
                            sportsContainer.AddFootballPlayer(footballPlayer);
                        }
                        break;

                    case "TEAM":
                        // Handle Team parsing
                        if (values.Length >= 4)
                        {
                            string teamName = values[1];
                            string city = values[2];
                            string coachName = values[3];
                            int gamesPlayed = int.Parse(values[4]);
                            Team team = new Team(teamName, city, coachName, gamesPlayed);
                            sportsContainer.AddTeam(team);
                        }
                        break;

                    default:
                        break; // Unknown type
                }
            }

            return sportsContainer;
        }

        public static void PrintSportsContainer(SportsContainer container)
        {
            Console.WriteLine("Teams:");
            foreach (var team in container.TeamName)
            {
                Console.WriteLine($"- Team: {team.City}, Games Played: {team.GamesPlayed}");
            }

            Console.WriteLine("\nBasketball Players:");
            foreach (var player in container.BasketballPlayers)
            {
                Console.WriteLine($"- Basketball Player: ID={player.Id}, Name={player.Name}, Team={player.Team}, Games Played={player.GamesPlayed}, Points={player.Points}");
            }

            Console.WriteLine("\nFootball Players:");
            foreach (var player in container.Football)
            {
                Console.WriteLine($"- Football Player: ID={player.Id}, Name={player.Name}, Team={player.Team}, Games Played={player.GamesPlayed}, Goals={player.Goals}");
            }
        }

        public static void PrintTeams(List<Team> teams)
        {
            Console.WriteLine("Pasirinkite is siu miestu:");
            foreach (var team in teams)
            {
                Console.WriteLine($"- {team.City}");
            }
        }

        public static void PrintPlayers(List<Basketball> basketballPlayers, double averageBasketballPoints)
        {
            Console.WriteLine("\nKrepsinis:");
            foreach (var player in basketballPlayers)
            {
                if (player.Score > averageBasketballPoints)
                {
                    Console.WriteLine($"Krepsininkas: {player.LastName}, Taskai: {player.Score}");
                }
            }
        }

        public static void PrintPlayers(List<Football> footballPlayers, double averageFootballGoals)
        {
            Console.WriteLine("\nFutbolas:");
            foreach (var player in footballPlayers)
            {
                if (player.Score > averageFootballGoals)
                {
                    Console.WriteLine($"Futbolininkas: {player.LastName}, ivarciai: {player.Score}");
                }
            }
        }
    }


}

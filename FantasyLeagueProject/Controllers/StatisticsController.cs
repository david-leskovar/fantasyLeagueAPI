using FantasyLeagueProject.DTOs.StatisticsDTOs;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeagueProject.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository repository;

        public StatisticsController(IStatisticsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetUserScores")]
        public ActionResult<List<UserScore>> GetUserScores()
        {


            var users = repository.GetUserScores();
            if (users == null) return BadRequest();

            List<UserScore> newList = new List<UserScore>();

            foreach (var user in users)
            {

                int score = 0;
                foreach (var historyTeam in user.HistoryTeams)
                {

                    score += historyTeam.Points;
                }

                Console.WriteLine(user.Username);
                Console.WriteLine(score);

                var scoreToAdd = new UserScore() { UserName = user.Username, Score = score };

                newList.Add(scoreToAdd);


            }

            return Ok(newList);

        }


        [HttpGet("GetPlayersWithMostKills")]

        public ActionResult<List<PlayerScoreKills>> GetPlayersWithTheirKills()
        {

            var players = repository.GetPlayers();

            var listOfPlayers = new List<PlayerScoreKills>();

            foreach (var player in players)
            {

                int kills = 0;
                foreach (var performance in player.Performances)
                {

                    kills += performance.Kills;

                }

                listOfPlayers.Add(new PlayerScoreKills() { Name = player.Name, Kills = kills });

            }

            var ordererList = listOfPlayers.OrderByDescending(u => u.Kills).ThenBy(u => u.Name).Take(5);

            return Ok(ordererList);
        }


        [HttpGet("GetPlayersWithMostDeaths")]
        public ActionResult<List<PlayerScoreDeaths>> GetPlayersWithTheirDeaths()
        {

            var players = repository.GetPlayers();

            var listOfPlayers = new List<PlayerScoreDeaths>();

            foreach (var player in players)
            {

                int deaths = 0;
                foreach (var performance in player.Performances)
                {

                    deaths += performance.Deaths;

                }

                listOfPlayers.Add(new PlayerScoreDeaths() { Name = player.Name, Deaths = deaths });

            }

            var ordererList = listOfPlayers.OrderByDescending(u => u.Deaths).ThenBy(u => u.Name).Take(5);

            return Ok(ordererList);
        }

        [HttpGet("GetPlayerWithHighestScore")]
        public ActionResult<List<PlayerScore>> GetPlayersWithHighestScore()
        {

            var players = repository.GetPlayers();

            var listOfPlayers = new List<PlayerScore>();

            foreach (var player in players)
            {

                int score = 0;
                foreach (var performance in player.Performances)
                {

                    score += performance.Points;

                }

                listOfPlayers.Add(new PlayerScore() { Name = player.Name, Score = score });

            }

            var ordererList = listOfPlayers.OrderByDescending(u => u.Score).ThenBy(u => u.Name).Take(5);

            return Ok(ordererList);
        }



        [HttpGet("GetUserTeams")]
        public ActionResult<User> GetUserTeams(string username) { 
        
            var user = repository.GetUser(username);
            if (user == null) return BadRequest("User not found");
            return Ok(user);



        
        }

        [HttpGet("GetNumberOfPlayersPicked")]

        public ActionResult GetNumberOfPicked() {

            Dictionary<string, int> map = new();

            var users = repository.GetUsersWithHistoryTeams();

            foreach (var user in users) {


                foreach (var historyTeam in user.HistoryTeams) {


                    foreach (var player in historyTeam.Players) {

                        if (map.ContainsKey(player.Name))
                        {
                            map[player.Name]++;
                        }
                        else { 
                            map.Add(player.Name, 1);
                        }
                    }

                
                }
            
            }

            return Ok(map);


        
        }




    }

}
    








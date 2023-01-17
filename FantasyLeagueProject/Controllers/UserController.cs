using FantasyLeagueProject.DTOs;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FantasyLeagueProject.Controllers
{
    [Authorize(Roles = "Member")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IPostRepository postRepository;
        private readonly IConfiguration configuration;

        public UserController(IUserRepository userRepository, IPostRepository postRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.configuration = configuration;
        }


  




        [HttpPost("Join tournament")]

        public ActionResult JoinTournament(List<string> players)
        {

            if (userRepository.TournamentIsActive()) return BadRequest("Can't join an ongoing league");
            if (players.Count != 5)
                return BadRequest("There needs to be 5 players");


            var tournament = userRepository.GetLeagueDetails();
            if (tournament == null) return BadRequest("Tournament doesn't exist");

            double budget = 0;
            
            foreach(var player in players)
            {
                var playerToAdd = userRepository.GetPlayerByUsername(player);
                if (playerToAdd == null) return BadRequest("Error joining the league");
                budget += playerToAdd.Cost;
            }

            if (budget > tournament.Budget) return BadRequest("Budget not within limit!");


            string userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null) { return BadRequest("User not found"); };

            var user = userRepository.GetUser(userId);
            if (user == null) return BadRequest("User not found");

            if (userRepository.UserInTournament(user)) return BadRequest("User has already joined");


            if (userRepository.JoinLeague(user, players)) return Ok();

            return BadRequest("There was en error joing the league");


        }

        [HttpGet("GetLeaguePlayers")]

        public ActionResult GetPlayers() {

            var tournament = userRepository.GetLeagueDetails();
            if (tournament == null) return BadRequest("Tournament doesn't exist");

            return Ok(tournament.Players);


        }



        [AllowAnonymous]
        [HttpGet("GetDetails")]

        public ActionResult<TournamentReturn> GetTournamentDetails()
        {

            var tournament = userRepository.GetLeagueDetails();
            if (tournament == null) return new TournamentReturn() { Id = null,Name="Tournament doesn't exist" };


            string userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null) { return BadRequest("User not found"); };

            var user = userRepository.GetUser(userId);
            if (user == null) return BadRequest("User not found");

            var userHasJoined = false;


            if (tournament.Users.Contains(user)) { userHasJoined = true; }



            var tournamentToReturn = new TournamentReturn()
            {
                Id = tournament.Id,
                Name = tournament.Name,
                NumberOfRounds = tournament.NumberOfRounds,
                CurrentRound = tournament.CurrentRound,
                ChangeTeamEnabled = tournament.ChangeTeamEnabled,
                IsActive = tournament.IsActive,
                IsCompleted = tournament.IsCompleted,
                Users = tournament.Users,
                Players = tournament.Players,
                HistoryTeams = tournament.HistoryTeams,
                Budget = tournament.Budget,
                UserHasJoined = userHasJoined

            };

            return Ok(tournamentToReturn);




        }

        [HttpPut("LikePost")]
        public ActionResult LikePost(Guid postId)
        {

            string userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null) { return BadRequest("User not found"); };

            var user = userRepository.GetUser(userId);
            if (user == null) return BadRequest("User not found");

            if (postRepository.LikePost(user, postId)) return Ok();

            return BadRequest("There was an error liking the post");



        }


        [HttpPut("UnLikePost")]

        public ActionResult UnLikePost(Guid postId)
        {

            string userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null) { return BadRequest("User not found"); };

            var user = userRepository.GetUser(userId);
            if (user == null) return BadRequest("User not found");

            if(postRepository.UnLikePost(user, postId)) return Ok();
            return BadRequest("There was an error unliking the post");



        }



    }
}

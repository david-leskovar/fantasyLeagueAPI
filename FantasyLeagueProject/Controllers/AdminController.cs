using FantasyLeagueProject.Data;
using FantasyLeagueProject.DTOs;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeagueProject.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository repository;
        private readonly IConfiguration configuration;
        private readonly DataContext context;

        public  AdminController(IAdminRepository repository, IConfiguration configuration,DataContext context)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.context = context;
        }


        [HttpGet("NextRound")]
        public ActionResult GoToNextRound() {



            if (!repository.TournamentExists())
            {
                return BadRequest("Tournament doesn't exist!");
            }


            var tournament = repository.GetTournament();




            if (!tournament!.IsActive) return BadRequest("Can't go to next round if the league hasn't started yet!");
            if (!tournament.RoundPerfomancesAdded) return BadRequest("Can't go to the next round until the performances have been added");
            if (tournament.IsCompleted) return BadRequest("Can't go to next round, tournament is finished!");


            foreach (var user in tournament.Users) {


                int calculatedScore = 0;
                foreach (var player in user.Players) {

                    calculatedScore += player.Performances.Where(u => u.RoundNumber == tournament.CurrentRound).First().Points;
                
                }



                var historyTeam = new HistoryTeam() { Id = Guid.NewGuid(), UserId = user.Id, RoundNumber = tournament.CurrentRound, Players = user.Players,Points=calculatedScore };
                context.HistoryTeams.Add(historyTeam);
                user.HistoryTeams.Add(historyTeam);
                tournament.HistoryTeams.Add(historyTeam);
            
            }

            tournament.CurrentRound++;
            tournament.RoundPerfomancesAdded = false;

            if (tournament.CurrentRound > tournament.NumberOfRounds) {
                tournament.IsCompleted = true;
            }

            if (context.SaveChanges() > 0) return Ok();

            return BadRequest("Something went wrong");

        }




        [HttpPost("Add player to tournament")]

        public ActionResult AddPlayerToTournament(CreatePlayerDTO request) {

            if (!repository.TournamentExists()) {
                return BadRequest("Tournament doesn't exist!");
            }

            var tournament = repository.GetTournament();
            if (tournament!.IsActive) return BadRequest("Can't add players after the tournament has already started!");


            var PlayerToCreate = new Player() { Id = Guid.NewGuid(), Name = request.Name, Cost = request.Cost };
            if (repository.AddPlayerToTournament(PlayerToCreate)) return Ok();
            else {
                return BadRequest("There was a problem adding a player to the tournament");
            }


          
        
        }

        [HttpPost("AddPlayerPerformaces")]

        public ActionResult AddPlayerPerformances(List<PerformanceDTO> performances) {


           

            if (!repository.TournamentExists()) return BadRequest("Tournament doesn't exist");

            var tournament = repository.GetTournament();

            if (!tournament.IsActive) return BadRequest("Tournament has to be active if you want to add scores!");
            if (tournament!.RoundPerfomancesAdded) return BadRequest("This round perfomance have already been added!");
            if (tournament.IsCompleted) return BadRequest("Can't add performances, tournament is finished!");

            if (performances.Count() != tournament.Players.Count) { return BadRequest("Not all player scores have been added!"); }


            var roundNumber = repository.GetRoundNumber();

            foreach (var performance in performances)
            {


                var playerExists = context.Players.Where(u => u.Name == performance.Name).Any();
                if (!playerExists) { return BadRequest("Player not found"); }

                var player = context.Players.Where(u => u.Name == performance.Name).FirstOrDefault();

                var calculatedScore = performance.Kills * 2 + performance.Assists - performance.Deaths;

                var performanceToAdd = new Performance() { Kills = performance.Kills, Assists = performance.Assists, Deaths = performance.Deaths, RoundNumber = roundNumber, PlayerId = player.Id, Id = new Guid(),Points=calculatedScore };

                context.Performances.Add(performanceToAdd);
                player.Performances.Add(performanceToAdd);
                

            }

            tournament.RoundPerfomancesAdded = true;

            if (context.SaveChanges() > 0) {
        
                return Ok();


            }

            return BadRequest("There was an error adding performances");
        
        }


  



        [HttpPost("Create tournament")]

        public ActionResult CreateTournament(TournamentDTO request) {

            if (repository.TournamentExists()) return BadRequest("Tournament already exists");

            var Tourney = new Tournament() { Id = Guid.NewGuid(), NumberOfRounds = request.NumberOfRounds, Name = request.Name,Budget=request.Budget,IsActive=false,IsCompleted=false };

            if (repository.CreateTournament(Tourney)) return Ok();

            return BadRequest("There was en error creating the tournament");
        
        }

        [HttpDelete("DeleteTournament")]

        public ActionResult DeleteTournament()
        {
            if (!repository.TournamentExists()) return BadRequest("Tournament doesn't exist");
            
       

            if (repository.DeleteTournament()) return Ok();
            return BadRequest("There was a problem deleting the tournament");
            

        }

        [HttpGet("GetTournamentPlayers")]

        public ActionResult GetPlayers()
        {

            var tournament = repository.GetTournament();
            if (tournament == null) return BadRequest("Tournament doesn't exist");

            return Ok(tournament.Players);


        }


        [HttpGet("GetTournamentDetails")]
        public ActionResult<Tournament> GetTournamentDetails(){

            var tournament = repository.GetAdminDashBoardTournament();
            if (tournament == null) return BadRequest("Tournament doesn't exist");

            return Ok(tournament);

           
        
        }

        [HttpGet("StartLeague")]

        public ActionResult StartLeague() {

            var tournament = repository.GetTournament();
            if (tournament.IsActive) return BadRequest("League is already active"); 
            if (tournament == null) return BadRequest("League does not exist");

            if (repository.StartLeague(tournament)) return Ok();
            return BadRequest("There was an error starting the league");
        
        }

        [HttpPost("AddPost")]

        public ActionResult AddNewPost(PostDTO request) {

            Post postToAdd = new Post() { Id = Guid.NewGuid(),Content=request.Content, Title = request.Title, SubTitle = request.SubTitle, ImageURL = request.ImageURL };

            if (repository.AddPost(postToAdd)) {
                return Ok();
            }
            return BadRequest("There was a problem creating a post");

        
        }

        [HttpDelete("DeletePost")]

        public ActionResult DeletePost(Guid id)
        {
            if (repository.DeletePost(id)) return Ok();
            return BadRequest("There was an error deleting the post");
        }


    }
}

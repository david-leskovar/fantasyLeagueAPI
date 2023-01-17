using FantasyLeagueProject.Data;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;


namespace FantasyLeagueProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public Tournament? GetLeagueDetails()
        {
            if (!context.Tournaments.Any()) { return null; }

            return context.Tournaments.Include(u=>u.HistoryTeams).Include(u=>u.Users).Include(u=>u.Players).First();

        }

        public User? GetUser(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) { return null; }
            
            return user;
        }

        public bool JoinLeague(User user,List<string> players)
        {
            if (!context.Tournaments.Any()) { return false; }

            var tournament = context.Tournaments.Include(u=>u.Players).First();

            var Joinee = context.Users.FirstOrDefault(u=>u.Equals(user));
            if (Joinee == null) return false;

            foreach (var player in players) { 
            
                var playerToAdd = tournament.Players.FirstOrDefault(u => u.Name==player);
                if(playerToAdd == null) { return false; }
                Joinee.Players.Add(playerToAdd);

            }

            tournament.Users.Add(Joinee);

            if (context.SaveChanges() > 0) return true;
            else { return false; }

        }

        public bool TournamentIsActive()
        {
            if (!context.Tournaments.Any()) { return false; }

            var tournament = context.Tournaments.First();

            if (tournament.IsActive) return true;

            return false;



        }


        public bool CanUpdateTeams() {
            if (!context.Tournaments.Any()) { return false; }
            var tournament = context.Tournaments.First();
            if (tournament.ChangeTeamEnabled) return true;
            return false;
        }


        public bool UpdateTeam()
        {
            throw new NotImplementedException();
        }

        public bool UserInTournament(User user)
        {
            var tournament = context.Tournaments.Include(u => u.Users).First();

            if (tournament.Users.Contains(user)) return true;
            else return false;

        }

        public int getRoundNUmber() {

            var tournament = context.Tournaments.Include(u => u.Users).First();
            return tournament.CurrentRound;

        }

        public Player? GetPlayerByUsername(string username)
        {
            

            try {
                var tournament = context.Tournaments.Include(u => u.Players).First();
                var player = tournament.Players.Find(u => u.Name == username);
                return player;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            

        }
    }
}

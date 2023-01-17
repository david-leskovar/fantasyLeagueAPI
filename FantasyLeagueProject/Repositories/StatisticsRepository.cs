using FantasyLeagueProject.Data;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantasyLeagueProject.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DataContext context;

        public StatisticsRepository(DataContext context)
        {
            this.context = context;
        }

        public List<Player> GetPlayers()
        {


            if (!context.Tournaments.Any()) return null;
            var tournament = context.Tournaments.Include(u=>u.Players).ThenInclude(u=>u.Performances).First();
            return tournament.Players;
            



        }

        public List<User>? GetUserScores()
        {

            if (!context.Tournaments.Any()) return null;


            return context.Tournaments.Include(u=>u.Users).ThenInclude(u=>u.HistoryTeams).First().Users.ToList();

            
        }

        public User? GetUser(string username) {

            if (!context.Tournaments.Any()) return null;
            var tournament = context.Tournaments.Include(u=>u.Users).ThenInclude(u=>u.HistoryTeams).ThenInclude(u => u.Players).ThenInclude(u => u.Performances).First();

            Console.WriteLine(username);

            var user = tournament.Users.Find(u => u.Username == username);
            return user;


        }

        public List<User>? GetUsersWithHistoryTeams()
        {
            if (!context.Tournaments.Any()) return null;

            var users = context.Users.Include(u => u.HistoryTeams).ThenInclude(u => u.Players).ToList();
            return users;


        }
    }
}

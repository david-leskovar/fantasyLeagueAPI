using FantasyLeagueProject.Entities;

namespace FantasyLeagueProject.Interfaces
{
    public interface IStatisticsRepository
    {

        public List<User> GetUserScores();
        public List<Player> GetPlayers();
        public User? GetUser(string username);
        public List<User> GetUsersWithHistoryTeams();



    }
}

using FantasyLeagueProject.Entities;

namespace FantasyLeagueProject.Interfaces
{
    public interface IUserRepository
    {

        public bool JoinLeague(User user, List<string> players);
        public bool UpdateTeam();
        public Tournament? GetLeagueDetails();
        public User GetUser(string username);

        public bool TournamentIsActive();
        public bool UserInTournament(User user);

        public bool CanUpdateTeams();

        public int getRoundNUmber();
        public Player? GetPlayerByUsername(string username);
    }
}

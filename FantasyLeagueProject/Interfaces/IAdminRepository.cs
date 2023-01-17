using FantasyLeagueProject.Entities;

namespace FantasyLeagueProject.Interfaces
{
    public interface IAdminRepository
    {

        public bool CreateTournament(Tournament tournament);
        public bool AddPlayerToTournament(Player player);
        public bool SetTournamentEnabledTeamChange();
        public bool TournamentExists();
        public bool StartLeague(Tournament tournament);
        public Tournament? GetTournament();
        public bool DeleteTournament();
        public int GetRoundNumber();
        public User? GetUserByID(Guid id);
        public Tournament? GetAdminDashBoardTournament();
        public bool AddPost(Post post);

        public bool DeletePost(Guid postID);





    }
}

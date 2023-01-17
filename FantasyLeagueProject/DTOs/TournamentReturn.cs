using FantasyLeagueProject.Entities;

namespace FantasyLeagueProject.DTOs
{
    public class TournamentReturn
    {

        public Guid? Id { get; set; }

        public int NumberOfRounds { get; set; }

        public bool UserHasJoined { get; set; }

        public int Budget { get; set; }

        public int CurrentRound { get; set; }
        public bool ChangeTeamEnabled { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } 

        public List<User> Users { get; set; } = new List<User>();

        public List<Player> Players { get; set; } = new List<Player>();

        public List<HistoryTeam> HistoryTeams { get; set; } = new List<HistoryTeam>();
    }
}

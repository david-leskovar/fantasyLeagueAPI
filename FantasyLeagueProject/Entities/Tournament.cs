namespace FantasyLeagueProject.Entities
{
    public class Tournament
    {
        public Guid Id { get; set; }

        public int NumberOfRounds { get; set; }

        public int Budget { get; set; } = 10;

        public int CurrentRound { get; set; } = 1;
        public bool ChangeTeamEnabled {get;set;} = false;
        public bool IsCompleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool RoundPerfomancesAdded { get; set; } = false;
        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new List<User>();

        public List<Player> Players { get; set; } = new List<Player>();

        public List<HistoryTeam> HistoryTeams { get; set; } = new List<HistoryTeam>();

        


    }
}

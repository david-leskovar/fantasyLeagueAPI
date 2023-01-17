namespace FantasyLeagueProject.Entities
{
    public class HistoryTeam
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int RoundNumber { get; set; }
        public int Points { get; set; }

    }
}

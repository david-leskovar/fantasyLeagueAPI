namespace FantasyLeagueProject.Entities
{
    public class Performance
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int RoundNumber { get; set; }
        public int Points { get; set; }

        
    }
}

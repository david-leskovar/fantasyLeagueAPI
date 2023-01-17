using FantasyLeagueProject.Entities;

namespace FantasyLeagueProject.DTOs
{
    public class TournamentDTO
    {

        public int NumberOfRounds { get; set; }
        public int Budget { get; set; } = 10;
        public string Name { get; set; } = string.Empty;


    }
}

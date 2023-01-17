using System.Text.Json.Serialization;

namespace FantasyLeagueProject.Entities
{
    public class Player
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double Cost { get; set; }

        public List<Performance> Performances { get; set; } = new List<Performance>();


        [JsonIgnore]
        public List<HistoryTeam> HistoryTeams { get; set; } = new List<HistoryTeam>();

        [JsonIgnore]
        public List<User> Users { get; set; } = new List<User>();


    }
}

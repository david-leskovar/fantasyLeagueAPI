using System.Text.Json.Serialization;

namespace FantasyLeagueProject.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set;  }
        public List<Player>? Players { get; set; } = new List<Player>();
        public List<HistoryTeam>? HistoryTeams { get; set; } = new List<HistoryTeam>();

        [JsonIgnore]
        public List<Post> LikedPosts { get; set; } = new List<Post>();


    }
}

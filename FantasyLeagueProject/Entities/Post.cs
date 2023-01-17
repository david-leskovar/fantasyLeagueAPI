namespace FantasyLeagueProject.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public List<User> LikedByUsers { get; set; } = new List<User>();

    }
}

namespace FantasyLeagueProject.DTOs
{
    public class PostReturnDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public int NumberOfLikes { get; set; } = 0;

    }
}

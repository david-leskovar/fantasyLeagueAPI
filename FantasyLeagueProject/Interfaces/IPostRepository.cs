using FantasyLeagueProject.Entities;

namespace FantasyLeagueProject.Interfaces
{
    public interface IPostRepository
    {

        public IEnumerable<Post> GetPosts();
        public bool LikePost(User user, Guid postID);
        public bool UnLikePost(User user, Guid postID);

        public Post? GetPost(Guid id);


    }
}

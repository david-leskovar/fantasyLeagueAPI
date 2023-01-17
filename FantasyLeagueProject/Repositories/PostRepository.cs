using FantasyLeagueProject.Data;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantasyLeagueProject.Repositories
{
    public class PostRepository : IPostRepository


    {
        private readonly DataContext context;
        public PostRepository(DataContext context)
        {
            this.context = context;
        }



        public IEnumerable<Post> GetPosts()
        {
            return context.Posts.Include(u=>u.LikedByUsers).ToList();
        }

        public Post? GetPost(Guid id) {



            var posts = context.Posts.Include(u => u.LikedByUsers);

            try {
                return posts.Where(u => u.Id == id).First();
                
            }
            catch(Exception ex) {
                return null;
            }
            
        
        }


        public bool LikePost(User user,Guid postID)
        {
            var post = GetPost(postID);
            if (post == null) { return false;}

            if(post.LikedByUsers.Contains(user)) { return false; }

            post.LikedByUsers.Add(user);
            if (context.SaveChanges() > 0) return true;
            return false;
            
        }

        public bool UnLikePost(User user,Guid postID)
        {
            var post = GetPost(postID);
            if (post == null) { return false; }


            if (!post.LikedByUsers.Contains(user)) return false;
            post.LikedByUsers.Remove(user);
            if (context.SaveChanges() > 0) return true;
            return false;
        }
    }
}

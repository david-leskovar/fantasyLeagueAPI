using FantasyLeagueProject.DTOs;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FantasyLeagueProject.Controllers
{

    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class PostsContoller : ControllerBase
    {
        private readonly IPostRepository repository;

        public PostsContoller(IPostRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetPosts")]

        public ActionResult<IEnumerable<PostReturnDTO>> GetAllPosts() {


            var posts = repository.GetPosts();

            List<PostReturnDTO> ListToReturn= new List<PostReturnDTO>();
            foreach (var post in posts) {

                PostReturnDTO p = new PostReturnDTO() { Id = post.Id, Content = post.Content, SubTitle = post.SubTitle, Title = post.Title, ImageURL = post.ImageURL, NumberOfLikes = post.LikedByUsers.Count };
                ListToReturn.Add(p);
                
            
            }

            return Ok(ListToReturn);

        }


      
    }
}

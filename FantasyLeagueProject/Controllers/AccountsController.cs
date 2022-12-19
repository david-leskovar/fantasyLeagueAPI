using FantasyLeagueProject.DTOs;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FantasyLeagueProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository repository;
        private readonly IConfiguration configuration;

        public AccountsController(IAccountRepository repository,IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginDTO request)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var user = repository.GetUser(request.Username!);
            if (user == null) return BadRequest("User not found");

            if (!VerifyPasswordHash(request.Password!, user.PasswordHash!, user.PasswordSalt!)) return BadRequest("Wrong password");

            LoginReturn returnLogin = new LoginReturn() { Username=user.Username!,Token=CreateToken(user)};

            return Ok(returnLogin);




        }


        [HttpPost("Register")]
        public  ActionResult<User> Register(RegisterDTO request)



        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (repository.GetUser(request.Username!) != null) return BadRequest("Username is taken");


            CreatePasswordHash(request.Password!, out byte[] passwordHash, out byte[] passwordSalt);

            var userToCreate = new User() { Id = Guid.NewGuid(), Username = request.Username, PasswordHash = passwordHash, PasswordSalt = passwordSalt };

            if (repository.AddUser(userToCreate)) return Ok("User successfully created");
            else return BadRequest(ModelState);




        }

        [Authorize(Roles="Member")]
        [HttpGet("Random method")]

        public ActionResult GetSome() {


            string userId = User.FindFirst(ClaimTypes.Name)?.Value;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            var user = repository.GetUser(userId);
            if (user == null) return BadRequest("User not found");


            Console.WriteLine(user.Id);
            Console.WriteLine(user.Username);
            Console.WriteLine(role);

            return Ok(user.Username);
        
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {

            using (var hmac = new HMACSHA512()) {

                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {

            using (var hmac = new HMACSHA512(passwordSalt)) {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }

        private string CreateToken(User user) {

            List<Claim> claims = new List<Claim> {

                new Claim(ClaimTypes.Name,user.Username!),
                new Claim(ClaimTypes.Role,"Member")

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(100),signingCredentials:creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        
        
        }

        




    }
}
using FantasyLeagueProject.Entities;

namespace FantasyLeagueProject.Interfaces
{
    public interface IAccountRepository
    {

        public bool AddUser(User user);
        public User? GetUser(string username);

    }
}

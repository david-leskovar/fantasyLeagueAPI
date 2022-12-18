using FantasyLeagueProject.Data;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;

namespace FantasyLeagueProject.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private DataContext context;

        public AccountRepository(DataContext context) {
            this.context = context;
        }

        public bool AddUser(User user) {



            context.Users.Add(user);
            if (context.SaveChanges() > 0) return true;
            else return false;
        }

        public User? GetUser(string username) {


            var userToReturn = context.Users.FirstOrDefault(u => u.Username == username);
            if (userToReturn == null) return null;
            return userToReturn;
  
        }


    }
}

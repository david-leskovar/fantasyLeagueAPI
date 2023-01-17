using FantasyLeagueProject.Data;
using FantasyLeagueProject.Entities;
using FantasyLeagueProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantasyLeagueProject.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        private readonly DataContext context;

        public AdminRepository(DataContext context)
        {
            this.context = context;
        }

        public bool AddPlayerToTournament(Player player)
        {
            var tournament = context.Tournaments.Include(u=>u.Players).First();

            context.Players.Add(player);
            tournament.Players.Add(player);

            if (context.SaveChanges() > 0) { return true; }
            else { return false; }
        }


        public bool StartLeague(Tournament tournament) { 
            
            tournament.IsActive= true;
            if (context.SaveChanges() > 0) { return true; }
            else { return false; }

        }

        public bool CreateTournament(Tournament tournament)
        {
            context.Tournaments.Add(tournament);
            if (context.SaveChanges() > 0) { return true; }
            else { return false; }

        }

        public bool SetTournamentEnabledTeamChange()
        {
            var tournament = context.Tournaments.Include(u => u.Players).First();
            tournament.ChangeTeamEnabled = true;
            if (context.SaveChanges() > 0) { return true; }
            else { return false; }

        }

        public bool SetTournamentDisabledTeamChange() {

            var tournament = context.Tournaments.Include(u => u.Players).First();
            tournament.ChangeTeamEnabled = false;
            if (context.SaveChanges() > 0) { return true; }
            else { return false; }
        }

        public bool TournamentExists()
        {
            var tournamentExists = context.Tournaments.Any();
            if (tournamentExists) return true;
            return false;
        }

        public Tournament? GetTournament() {

            if (!TournamentExists()) return null;
            return context.Tournaments.Include(u=>u.Players).ThenInclude(u=>u.Performances).Include(u=>u.HistoryTeams).Include(u=>u.Users).ThenInclude(u=>u.Players).ThenInclude(u=>u.HistoryTeams).First();
            
        }

        public bool DeleteTournament() { 
        
            context.Tournaments.Remove(GetTournament());

            context.Players.RemoveRange(context.Players);

            context.Performances.RemoveRange(context.Performances);

            context.HistoryTeams.RemoveRange(context.HistoryTeams);


            if (context.SaveChanges() > 0) return true;
            return false;
            
        }

        public int GetRoundNumber()
        {

            var tournament = context.Tournaments.Include(u => u.Users).First();
            return tournament.CurrentRound;

        }

        public User? GetUserByID(Guid id) { 
        
            var user = context.Users.Find(id);
            if(user==null) return null;

            return user;
        
        
        }

        public Tournament? GetAdminDashBoardTournament()
        {

            if (!TournamentExists()) return null;
            var tournament = context.Tournaments.Include(u => u.Users).Include(u=>u.Players).First();
            return tournament;



        }
        public bool AddPost(Post post)
        {
            context.Posts.Add(post);
            if(context.SaveChanges()>0) return true;
            return false;
        }

        public bool DeletePost(Guid postID) { 
        
            var postToDelete = context.Posts.Find(postID);
            if(postToDelete==null) return false;
            context.Posts.Remove(postToDelete);

            if (context.SaveChanges() > 0) return true;
            return false;
            
        
        }
    }
}

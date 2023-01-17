using FantasyLeagueProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace FantasyLeagueProject.Data
{
    public class DataContext : DbContext
    {

        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("DBConnection"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<HistoryTeam> HistoryTeams { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Post> Posts { get; set; }


    }
}

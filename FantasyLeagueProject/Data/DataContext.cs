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

    }
}

using ApiMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMovies
{
    public class DBContext : DbContext
    {
        public DbSet<Ids> Ids { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new IdsConfig());
            builder.ApplyConfiguration(new MovieConfig());

            base.OnModelCreating(builder);
        }
    }
}

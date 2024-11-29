using Microsoft.EntityFrameworkCore;
using Movies.Model;

namespace Movies.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieDirector>()
                .HasKey(x => new { x.MovieId, x.DirectorId });

            modelBuilder.Entity<MovieDirector>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.MovieDirectors)
                .HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<MovieDirector>()
                .HasOne(x => x.Director)
                .WithMany(x => x.MovieDirectors)
                .HasForeignKey(x => x.DirectorId);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
    }
}

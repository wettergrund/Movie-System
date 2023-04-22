using Microsoft.EntityFrameworkCore;
using System.Linq;
using MovieAPI.Models;

namespace MovieAPI.connection
{
    public class RepositoryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=movieDb;Integrated Security=true;TrustServerCertificate=True");
        }

        public DbSet<User> User { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<UserMovie> UserMovie { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }

    }
}

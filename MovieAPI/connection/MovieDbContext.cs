using Microsoft.EntityFrameworkCore;
using System.Linq;
using MovieAPI.Models;

namespace MovieAPI.connection
{
    public class MovieDBContext : DbContext
    {

        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<User_genre_movie> UserGendreMovies { get; set; }
        public DbSet<v_userGenreInfo> v_userGenreInfo { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-D28JTEI; Initial Catalog=movieDb;Integrated Security=true;TrustServerCertificate=True");


        //}




    }
}

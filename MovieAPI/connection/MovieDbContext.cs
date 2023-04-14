using Microsoft.EntityFrameworkCore;
using System.Linq;
using MovieAPI.Models;

namespace MovieAPI.connection
{
    public class MovieDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-JE202T10; Initial Catalog=movieDb;Integrated Security=true;TrustServerCertificate=True");


        }




    }
}

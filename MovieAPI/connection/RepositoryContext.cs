﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using MovieAPI.Models;

namespace MovieAPI.connection
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }
        
        public DbSet<User> User { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<UserGenreMovie> UserGendreMovies { get; set; }
        public DbSet<v_userGenreInfo> v_userGenreInfo { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-D28JTEI; Initial Catalog=movieDb;Integrated Security=true;TrustServerCertificate=True");


        //}




    }
}
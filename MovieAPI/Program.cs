global using MovieAPI.Models;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MovieAPI.connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MovieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddDbContextPool<RepositoryContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            app.MapGet("/movie/{movie}", async (HttpContext httpContext, string movie) =>
            {
                // Search movie, repository pattern

                TMDBRepository TMDBRepo = new TMDBRepository();

                return await TMDBRepo.GetByTitle(movie);

               
            });

            app.MapGet("/moviebyID/{id}", async (HttpContext httpContext, int id) =>
            {
                // Search movie, repository pattern

                TMDBRepository TMDBRepo = new TMDBRepository();

                return await TMDBRepo.GetByID(id);


            });


            app.MapPost("/API/movie/create", async (Movie newMovie, int id) =>
            {
                RepositoryContext context = new RepositoryContext();


                MovieRepository movieRepo = new MovieRepository(context);

                var movieExist = movieRepo.GetByCondition(m => m.ExtID == id);

                if (movieExist.IsNullOrEmpty())
                {
                    //Create movie

                    //Get movie from TMDB by ID

                    TMDBRepository TMDBRepo = new TMDBRepository();

                    var movie = await TMDBRepo.GetByID(id);

                    newMovie.ExtID = movie.ExtID;
                    newMovie.Title = movie.Title;
                    newMovie.Link = $"https://www.themoviedb.org/movie/{newMovie.ExtID}-{newMovie.Title}";
                    newMovie.Description = movie.Overview;

                    movieRepo.Create(newMovie);
                    context.SaveChanges();

                    return newMovie;
                }
                return newMovie;
            });


            app.MapGet("/users", async () =>
            {
                RepositoryContext context = new RepositoryContext();
           

                UserRepository userRepo = new UserRepository(context);

                return userRepo.GetAll();
            });

            app.MapGet("/users/search", async (
                [FromQuery(Name = "name")] string name) =>
            {
                RepositoryContext context = new RepositoryContext();

                UserRepository userRepo = new UserRepository(context);
                var user = userRepo.GetByCondition(u => u.Name == name);

                return user.IsNullOrEmpty() ? Results.NotFound("User not found") : Results.Ok(user);

            });



            app.MapGet("/genre/{id}", async (int id) =>
            {
                RepositoryContext context = new RepositoryContext();
                //Get genres from DB by id, repository pattern

                GenreRepository genreRepo = new GenreRepository(context);

                return genreRepo.GetByCondition(g => g.Id == id);
            });


            app.MapGet("/genrebyuser", async (int userId) =>
            {
                //Get genres connected to a user ID


                RepositoryContext context = new RepositoryContext();

                UserGenreMovieRepository ugmRepo = new UserGenreMovieRepository(context);



                return ugmRepo.GetByCondition(ugm => ugm.UserID == userId);
            });

            app.MapGet("/getallgenre", async () =>
            {
                //Get genres connected to a user ID

                RepositoryContext context = new RepositoryContext();

                UserGenreMovieRepository ugmRepo = new UserGenreMovieRepository(context);



                return ugmRepo.GetAll();
            });


            app.Run();

        }
    }
}
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
            //builder.Services.AddDbContext<RepositoryContext>(options =>
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



            //app.MapGet("/movie/{movie}", async (HttpContext httpContext, string movie) =>
            //{

            //    string URL = $"https://api.themoviedb.org/3/search/movie?api_key=2b7d5bf25d89ca81c83f8d6a2ac12244&language=en-US&query={movie}&include_adult=false";
            //    using (var client = new HttpClient())
            //    {
            //        var response = await client.GetAsync(URL);
            //        response.EnsureSuccessStatusCode();

            //        var content = await response.Content.ReadAsStringAsync();
            //        dynamic? result = JsonConvert.DeserializeObject<Result>(content);


            //        return result;
            //    }

            //    //return result;
            //});


            app.MapGet("/movie/{movie}", async (HttpContext httpContext, string movie) =>
            {
                // Search movie, repository pattern

                TMDBRepository TMDBRepo = new TMDBRepository();

                return await TMDBRepo.GetData(movie);

                //return result;
            });

            //app.MapGet("/users", async (HttpContext httpContext) =>
            //{

            //    using (var context = new MovieDBContext())
            //    {
            //        var users = context.User;
            //        List<User> result = new List<User>(users);


            //        dynamic json = JsonConvert.SerializeObject(result);


            //        return result;
            //    }

            //})
            //.WithName("GetUsers");


            //app.MapGet("/users", async (RepositoryContext context) =>
            //    await context.User.ToArrayAsync());
            app.MapGet("/users", async (RepositoryContext context) =>
            {
                //Get all users in DB, repository pattern
                //Hämta alla personer i systemet

                UserRepository userRepo = new UserRepository(context);

                return userRepo.GetAll();
            });

            app.MapGet("/users/search", async (RepositoryContext context,
                [FromQuery(Name = "name")] string name) =>
            {
                UserRepository userRepo = new UserRepository(context);
                var user = userRepo.GetByCondition(u => u.Name == name);

                return user.IsNullOrEmpty() ? Results.NotFound("User not found") : Results.Ok(user);

                //var user = await context.User.Where(u => u.Name == name).FirstOrDefaultAsync();
                //return user != null ? Results.Ok(user) : Results.NotFound("User not found");
            });

            //app.MapGet("/genre/{id}", async (RepositoryContext context, int id) =>
            //    await context.v_userGenreInfo.Where(u => u.UID == id).ToArrayAsync());

            app.MapGet("/genre/{id}", async (RepositoryContext context, int id) =>
            {
                //Get genres from DB by id, repository pattern

                GenreRepository genreRepo = new GenreRepository(context);

                return genreRepo.GetByCondition(g => g.Id == id);
            });

            
            app.MapGet("/genrebyuser", async (RepositoryContext context, int userId) =>
            {
                //Get genres connected to a user ID

                
                

                UserGenreMovieRepository ugmRepo = new UserGenreMovieRepository(context);



                return ugmRepo.GetByCondition(ugm => ugm.UserID == userId);
            });

            app.MapGet("/getallgenre", async (RepositoryContext context) =>
            {
                //Get genres connected to a user ID



                UserGenreMovieRepository ugmRepo = new UserGenreMovieRepository(context);



                return ugmRepo.GetAll();
            });

            //app.MapGet("/genre/byID", async (MovieDBContext context,
            //    [FromQuery(Name = "Id")] int? id,
            //    [FromQuery(Name = "Name")] string? name
            //    ) =>
            //    {
            //       if(name != null)
            //        {
            //            return await context.v_userGenreInfo.Where(u => u.Name == name).ToArrayAsync();
            //        }
            //        else
            //        {

            //            return await context.v_userGenreInfo.Where(u => u.UID == id).ToArrayAsync();

            //        }


            //    });


            //app.MapGet("/genres", async (HttpContext httpContext) =>
            //{

            //    using (var context = new MovieDBContext()
            //    {
            //        var genres = context.Genre;
            //        List<Genre> result = new List<Genre>(genres);


            //        dynamic json = JsonConvert.SerializeObject(result);


            //        return result;
            //    }

            //})
            //.WithName("GetGenres");

            app.Run();

        }
    }
}
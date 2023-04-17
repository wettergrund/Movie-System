global using MovieAPI.Models;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MovieAPI.connection;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddDbContext<MovieDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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
                
                string URL = $"https://api.themoviedb.org/3/search/movie?api_key=2b7d5bf25d89ca81c83f8d6a2ac12244&language=en-US&query={movie}&include_adult=false";
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    dynamic? result = JsonConvert.DeserializeObject<Result>(content);


                    return result;
                }

                //return result;
            })
            .WithName("GetMovies");

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


            app.MapGet("/users", async (MovieDBContext context) =>
                await context.User.ToArrayAsync()).WithName("GetUsers");

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
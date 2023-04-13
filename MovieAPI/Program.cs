using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
               };

            //        app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            //        {
            //            var forecast = Enumerable.Range(1, 5).Select(index =>
            //                new WeatherForecast
            //                {
            //                    Date = DateTime.Now.AddDays(index),
            //                    TemperatureC = Random.Shared.Next(-20, 55),
            //                    Summary = summaries[Random.Shared.Next(summaries.Length)]
            //                })
            //                .ToArray();
            //            return forecast;
            //        })
            //        .WithName("GetWeatherForecast");

            app.MapGet("/movie/{movie}", async (HttpContext httpContext, string movie) =>
            {
                
                string URL = $"https://api.themoviedb.org/3/search/movie?api_key=2b7d5bf25d89ca81c83f8d6a2ac12244&language=en-US&query={movie}&include_adult=false";
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject<Result>(content);


                    return result;
                }

                //return result;
            })
    .WithName("GetMovies");

            app.Run();
        }
    }
}
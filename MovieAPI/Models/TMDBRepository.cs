using Microsoft.IdentityModel.Tokens;
using MovieAPI.connection;
using Newtonsoft.Json;

namespace MovieAPI.Models
{
    public class TMDBRepository : ITMDBRepository
    {
        private string Key()
        {
            var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            string? result = configuration["key"];

            if (result.IsNullOrEmpty())
            {
                return "No key";
            }
            else
            {

                return result;
            }

        }

        public TMDBRepository() { }

        public async Task<Result> GetByTitle(string title)
        {
            string URL = $"https://api.themoviedb.org/3/search/movie?api_key={Key()}&language=en-US&query={title}&include_adult=false";

            //https://api.themoviedb.org/3/search/movie?api_key=2cb182b792525c9bcfd1cf968eb35326&language=en-US&query=Titanic&page=1&include_adult=false
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(URL);
                //response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject<Result>(content);


                return result;
            }
        }

        public async Task<TMDBMovie> GetByID(int id)
        {
            string URL = $"https://api.themoviedb.org/3/movie/{id}?api_key={Key()}&language=en-US";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(URL);
                //response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject<TMDBMovie>(content);


                return result;
            }
        }

        public async Task<Result> GetByGenres(string genres)
        {
            string URL = $"https://api.themoviedb.org/3/discover/movie?api_key={Key()}&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_genres={genres}&with_watch_monetization_types=flatrate";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(URL);
                //response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject<Result>(content);


                return result;
            }
        }
    }
}

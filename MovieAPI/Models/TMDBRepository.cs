using MovieAPI.connection;
using Newtonsoft.Json;

namespace MovieAPI.Models
{
    public class TMDBRepository : ITMDBRepository
    {
        public TMDBRepository() { }

        public async Task<Result> GetByTitle(string title)
        {
            string URL = $"https://api.themoviedb.org/3/search/movie?api_key=2b7d5bf25d89ca81c83f8d6a2ac12244&language=en-US&query={title}&include_adult=false";
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
            string URL = $"https://api.themoviedb.org/3/movie/{id}?api_key=2b7d5bf25d89ca81c83f8d6a2ac12244&language=en-US";
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
            string URL = $"https://api.themoviedb.org/3/discover/movie?api_key=2b7d5bf25d89ca81c83f8d6a2ac12244&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_genres={genres}&with_watch_monetization_types=flatrate";
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

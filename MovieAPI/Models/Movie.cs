using Newtonsoft.Json;

namespace MovieAPI.Models
{
    public class Movie
    {
        private string _poster = "";
        public int ID { get; set; }

        [JsonProperty("original_title")]
        public string Title { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> Gendres { get; set; }

        [JsonProperty("vote_average")]
        public decimal AverageScore { get; set; }

        public string Overview { get; set; }

        [JsonProperty("poster_path")]
        public string poster { get; set; }
        
        public string posterM
        {
            get
            {
                _poster = $"https://image.tmdb.org/t/p/w500{poster}";
                return _poster;
            }
            set
            {
                _poster = value;
            }
        }
        public string posterS
        {
            get
            {
                _poster = $"https://image.tmdb.org/t/p/w200{poster}";
                return _poster;
            }
            set
            {
                _poster = value;
            }
        }

    }
}

using Newtonsoft.Json;

namespace MovieAPI.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [JsonProperty("original_title")]
        public string Title { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> Gendres { get; set; }

        [JsonProperty("vote_average")]
        public decimal AverageScore { get; set; }

        public string Overview { get; set; }

    }
}

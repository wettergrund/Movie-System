using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class TMDBMovie
    {
        private string _poster = "";

        [Key]
        public int ID { get; set; }

        [JsonProperty("id")]
        public int ExtID { get; set; }

        [JsonProperty("original_title")]
        public string Title { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Gendres { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> genreId { get; set; }

        [JsonProperty("vote_average")]
        public decimal AverageScore { get; set; }

        public string Overview { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }


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

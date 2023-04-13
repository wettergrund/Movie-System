using Newtonsoft.Json;

namespace MovieAPI
{
    public class Movie
    {
        [JsonProperty("id")]
        public int ExtID { get; set; }

        [JsonProperty("original_title")]
        public string Title { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> Gendres  { get; set; }

    }
}

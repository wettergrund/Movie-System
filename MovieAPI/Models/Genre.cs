using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Genre 
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonProperty("id")]
        public int ExtID { get; set; }

        public string Description { get; set; }

    }
}

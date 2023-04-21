using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Models
{
    [Keyless]
    public class Result
    {
        public string? Page { get; set; }
        public List<TMDBMovieList> Results { get; set; }
    }
}

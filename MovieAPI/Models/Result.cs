using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Models
{
    [Keyless]
    public class Result
    {
        public string? Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}

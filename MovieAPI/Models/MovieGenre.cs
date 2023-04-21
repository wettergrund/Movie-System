using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    [Table("movie_genre")]
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }
        public int MovieID { get; set; }
        public int GenreID { get; set; }
    }
}

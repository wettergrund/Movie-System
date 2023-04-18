using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    [Table("User_genre_movie")]
    public class UserGenreMovie
    {
        [Key]
        public int id { get; set; }
        public int UserID { get; set; }
        public int GenereID { get; set; }
        public int MovieID { get; set; }
    }
}

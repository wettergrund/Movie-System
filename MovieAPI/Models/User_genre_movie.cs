using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class User_genre_movie
    {
        [Key]
        public int id { get; set; }
        public int UserID { get; set; }
        public int GenereID { get; set; }
        public int MovieID { get; set; }
    }
}

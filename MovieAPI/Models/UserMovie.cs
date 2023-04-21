using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    [Table("user_movie")]
    public class UserMovie
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public int MovieID { get; set; }
        public int UserRating { get; set; }
    }
}

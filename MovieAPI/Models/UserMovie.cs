using Microsoft.EntityFrameworkCore;
using MovieAPI.connection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    [Table("user_movie")]
    public class UserMovie
    {
        [NotMapped]
        private readonly RepositoryContext _dbContext;

        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public int MovieID { get; set; }
        //public int UserRating { get; set; }
        public int UserRating { get; set; }


        public string? MovieName
        {
            get
            {


                MovieRepository movieRepo = new MovieRepository(_dbContext);

              

                return movieRepo.GetNameByID(MovieID);
            }
        }
    }
}

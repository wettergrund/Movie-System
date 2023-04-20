using Microsoft.Extensions.Options;
using MovieAPI.connection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    [Table("user_genre_movie")]
    public class UserGenreMovie
    {
        [NotMapped]
        private readonly RepositoryContext _dbContext;

        [Key]
        public int id { get; set; }
        public int UserID { get; set; }
        public int? MovieID { get; set; }

        public int GenreID { get; set; }
        public int UserRating { get; set;}

        public string GenreName
        {
            get
            {
          

                GenreRepository genreRepo = new GenreRepository(_dbContext);


                return genreRepo.GetNameByID(GenreID);
            }
        }


   
    }
}

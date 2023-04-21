using Microsoft.EntityFrameworkCore;
using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class MovieGenreRepository : RepositoryBase<MovieGenre>, IMovieGenreRepository
    {

        public MovieGenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }



    }
}

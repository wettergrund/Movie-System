using Microsoft.EntityFrameworkCore;
using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {

        public MovieRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }



    }
}

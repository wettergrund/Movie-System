using Microsoft.EntityFrameworkCore;
using MovieAPI.connection;

namespace MovieAPI.Models
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {

        public MovieRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public string GetNameByID(int id)
        {
            // Return movie title by DB ID
            RepositoryContext context = new RepositoryContext();

            MovieRepository movieRepo = new MovieRepository(context);
            var movies = movieRepo.GetByCondition(m => m.Id == id).ToList();




            return movies.First().Title;

        }


    }
}
